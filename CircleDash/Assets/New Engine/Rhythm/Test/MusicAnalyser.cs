using System.Collections.Generic;
using UnityEngine;

public class MusicAnalyser : MonoBehaviour
{
[Header("Audio")]
    public AudioClip audioClip;

    [Header("FFT")]
    public int fftSize = 2048;
    public int hopSize = 512;

    [Header("Сглаживание")]
    public int sm      = 120;
    public int finalSm = 64;

    [Header("Сигмоида")]
    public float kSig = 20f;
    public float x0   = 0.6f;

    [Header("Производная")]
    public int   derivSm  = 300;
    public float threshK  = 1.6f;

    [Header("Кластеризация границ")]
    public int minGroupSize = 15;
    public int clusterGap   = 50;

    [Header("Фильтрация участков")]
    public float minDuration = 5f;
    public float expandSec   = 2f;

    public RhythmManager manager;
    public int bpm;
    public List<Vector2> intenseSections = new List<Vector2>();

    // void Start()
    // {
    //     if (audioClip == null) { Debug.LogError("AudioClip не назначен!"); return; }

    //     var sw = System.Diagnostics.Stopwatch.StartNew();
    //     intenseSections = Analyse(audioClip, fftSize, hopSize, sm, finalSm,
    //                               kSig, x0, derivSm, threshK,
    //                               minGroupSize, clusterGap, minDuration, expandSec);
    //     sw.Stop();

    //     Debug.Log($"Анализ: {sw.ElapsedMilliseconds} мс | участков: {intenseSections.Count}");
    //     foreach (var s in intenseSections)
    //         Debug.Log($"  {s.x:F2}s — {s.y:F2}s  [{s.y - s.x:F2}s]");
    //     manager.X2_Sections = intenseSections;
    //     StartCoroutine(manager.ManageFullSpeed());
    //     bpm = UniBpmAnalyzer.AnalyzeBpm(audioClip);
    // }

    public static (List<Vector2> sections, int bpm) AnalyseClip(AudioClip clip)
    {
        var sections = Analyse(clip, 2048, 512, 120, 64,
                            20f, 0.6f, 300, 1.6f,
                            15, 50, 5f, 2f);
        int bpm = UniBpmAnalyzer.AnalyzeBpm(clip);
        return (sections, bpm);
    }


    public static List<Vector2> Analyse(
        AudioClip clip,
        int fftSize, int hopSize,
        int sm, int finalSm,
        float kSig, float x0,
        int derivSm, float threshK,
        int minGroupSize, int clusterGap,
        float minDuration, float expandSec)
    {
        float[] mono   = ToMono(clip);
        int sampleRate = clip.frequency;
        int nFrames    = (mono.Length - fftSize) / hopSize;
        float secPerFrame = (float)hopSize / sampleRate;
        float[] hann   = MakeHann(fftSize);

        float[] time_fft     = new float[nFrames];
        float[] rms_vals     = new float[nFrames];
        float[] spectral_sum = new float[nFrames];

        // ── 1. FFT ───────────────────────────────────────────────
        for (int i = 0; i < nFrames; i++)
        {
            int offset = i * hopSize;
            float[] frame = new float[fftSize];
            double rmsSum = 0;
            for (int j = 0; j < fftSize; j++)
            {
                frame[j] = mono[offset + j] * hann[j];
                rmsSum += frame[j] * frame[j];
            }

            float[] mag = FFTMagnitude(frame);

            time_fft[i]     = (float)offset / sampleRate;
            rms_vals[i]     = Mathf.Sqrt((float)(rmsSum / fftSize));
            spectral_sum[i] = Sum(mag);
        }

        // ── 2. Сглаживание ───────────────────────────────────────
        float[] r1 = MovMean(rms_vals,     sm);
        float[] r2 = MovMean(spectral_sum, sm);

        // ── 3. Комбинированный сигнал ────────────────────────────
        float[] r1n = Normalize(r1);
        float[] r2n = Normalize(r2);

        float[] combined_raw = new float[nFrames];
        for (int i = 0; i < nFrames; i++)
            combined_raw[i] = r1n[i] * r2n[i];

        float[] combined = new float[nFrames];
        for (int i = 0; i < nFrames; i++)
            combined[i] = 1f / (1f + Mathf.Exp(-kSig * (combined_raw[i] - x0)));
        combined = Normalize(combined);

        float[] smoothed = MovMean(combined, finalSm);

        // ── 4. Производная ───────────────────────────────────────
        float[] deriv = new float[nFrames];
        deriv[0] = smoothed[1] - smoothed[0];
        for (int i = 1; i < nFrames; i++)
            deriv[i] = smoothed[i] - smoothed[i - 1];

        float[] derivSmooth = MovMean(deriv, derivSm);

        float std = StdDev(derivSmooth);
        float thresh = threshK * std;

        var risingIdx  = new List<int>();
        var fallingIdx = new List<int>();
        for (int i = 0; i < nFrames; i++)
        {
            if (derivSmooth[i] >  thresh) risingIdx.Add(i);
            if (derivSmooth[i] < -thresh) fallingIdx.Add(i);
        }

        // ── 5. Кластеризация границ ──────────────────────────────
        int[] riseEdges = ClusterEdges(risingIdx,  true,  minGroupSize, clusterGap);
        int[] fallEdges = ClusterEdges(fallingIdx, false, minGroupSize, clusterGap);

        // ── 6. Фильтр: два подъёма подряд → второй ───────────────
        var filteredRise = new List<int>();
        foreach (int re in riseEdges)
        {
            float tRise = time_fft[re];
            if (filteredRise.Count == 0)
            {
                filteredRise.Add(re);
            }
            else
            {
                float tPrev = time_fft[filteredRise[filteredRise.Count - 1]];
                bool hasFallBetween = false;
                foreach (int fe in fallEdges)
                    if (time_fft[fe] > tPrev && time_fft[fe] < tRise) { hasFallBetween = true; break; }

                if (!hasFallBetween)
                    filteredRise[filteredRise.Count - 1] = re;
                else
                    filteredRise.Add(re);
            }
        }
        riseEdges = filteredRise.ToArray();

        // ── 7. Фильтр: два спада подряд → первый ─────────────────
        var filteredFall = new List<int>();
        foreach (int fe in fallEdges)
        {
            float tFall = time_fft[fe];
            if (filteredFall.Count == 0)
            {
                filteredFall.Add(fe);
            }
            else
            {
                float tPrev = time_fft[filteredFall[filteredFall.Count - 1]];
                bool hasRiseBetween = false;
                foreach (int re in riseEdges)
                    if (time_fft[re] > tPrev && time_fft[re] < tFall) { hasRiseBetween = true; break; }

                if (hasRiseBetween)
                    filteredFall.Add(fe);
            }
        }
        fallEdges = filteredFall.ToArray();

        // ── 8. Собираем пары и строим intenseSections ─────────────
        int expandFrames = Mathf.RoundToInt(expandSec / secPerFrame);
        var sections = new List<Vector2>();

        foreach (int re in riseEdges)
        {
            float tRise = time_fft[re];

            int feMatch = -1;
            foreach (int fe in fallEdges)
                if (time_fft[fe] > tRise) { feMatch = fe; break; }
            if (feMatch < 0) continue;

            float tFall = time_fft[feMatch];
            if (tFall - tRise < minDuration) continue;

            int idxStart = Mathf.Max(0, re - 1);
            int idxEnd   = Mathf.Min(nFrames - 1, feMatch + expandFrames);

            sections.Add(new Vector2(time_fft[idxStart], time_fft[idxEnd]));
        }

        return sections;
    }

    // ── Вспомогательные функции ───────────────────────────────────

    static int[] ClusterEdges(List<int> idx, bool pickLast, int minSize, int gap)
    {
        var edges  = new List<int>();
        if (idx.Count == 0) return edges.ToArray();

        var groups = new List<List<int>> { new List<int> { idx[0] } };
        for (int k = 1; k < idx.Count; k++)
        {
            if (idx[k] - idx[k - 1] < gap)
                groups[groups.Count - 1].Add(idx[k]);
            else
                groups.Add(new List<int> { idx[k] });
        }

        foreach (var g in groups)
        {
            if (g.Count < minSize) continue;
            int midIdx = g.Count / 2;
            edges.Add(g[midIdx]);
        }

        return edges.ToArray();
    }
    static float[] MovMean(float[] src, int k)
    {
        float[] dst = new float[src.Length];
        for (int i = 0; i < src.Length; i++)
        {
            int lo = Mathf.Max(0, i - k / 2);
            int hi = Mathf.Min(src.Length - 1, i + k / 2);
            float sum = 0f; int cnt = 0;
            for (int j = lo; j <= hi; j++) { sum += src[j]; cnt++; }
            dst[i] = sum / cnt;
        }
        return dst;
    }

    static float[] Normalize(float[] arr)
    {
        float mn = arr[0], mx = arr[0];
        foreach (float v in arr) { if (v < mn) mn = v; if (v > mx) mx = v; }
        float range = mx - mn;
        float[] out_ = new float[arr.Length];
        for (int i = 0; i < arr.Length; i++)
            out_[i] = range < 1e-9f ? 0f : (arr[i] - mn) / range;
        return out_;
    }

    static float StdDev(float[] arr)
    {
        double mean = 0;
        foreach (float v in arr) mean += v;
        mean /= arr.Length;
        double variance = 0;
        foreach (float v in arr) variance += (v - mean) * (v - mean);
        return (float)System.Math.Sqrt(variance / arr.Length);
    }

    static float Sum(float[] arr)
    {
        float s = 0f;
        foreach (float v in arr) s += v;
        return s;
    }

    static float[] ToMono(AudioClip clip)
    {
        float[] raw = new float[clip.samples * clip.channels];
        clip.GetData(raw, 0);
        if (clip.channels == 1) return raw;
        float[] mono = new float[clip.samples];
        for (int i = 0; i < clip.samples; i++)
        {
            float s = 0f;
            for (int c = 0; c < clip.channels; c++) s += raw[i * clip.channels + c];
            mono[i] = s / clip.channels;
        }
        return mono;
    }

    static float[] FFTMagnitude(float[] frame)
    {
        int n = frame.Length, bits = (int)System.Math.Log(n, 2);
        float[] re = (float[])frame.Clone(), im = new float[n];
        for (int i = 0; i < n; i++)
        {
            int rev = 0, x = i;
            for (int b = 0; b < bits; b++) { rev = (rev << 1) | (x & 1); x >>= 1; }
            if (rev > i) { float t = re[i]; re[i] = re[rev]; re[rev] = t; }
        }
        for (int len = 2; len <= n; len <<= 1)
        {
            double ang = -2.0 * System.Math.PI / len;
            float wr = (float)System.Math.Cos(ang), wi = (float)System.Math.Sin(ang);
            for (int i = 0; i < n; i += len)
            {
                float cr = 1f, ci = 0f;
                for (int j = 0; j < len / 2; j++)
                {
                    float ur = re[i+j], ui = im[i+j];
                    float vr = re[i+j+len/2]*cr - im[i+j+len/2]*ci;
                    float vi = re[i+j+len/2]*ci + im[i+j+len/2]*cr;
                    re[i+j] = ur+vr; im[i+j] = ui+vi;
                    re[i+j+len/2] = ur-vr; im[i+j+len/2] = ui-vi;
                    float nr = cr*wr - ci*wi; ci = cr*wi + ci*wr; cr = nr;
                }
            }
        }
        float[] mag = new float[n / 2];
        for (int k = 0; k < n / 2; k++)
            mag[k] = Mathf.Sqrt(re[k]*re[k] + im[k]*im[k]) / n;
        return mag;
    }

    static float[] MakeHann(int size)
    {
        float[] w = new float[size];
        for (int i = 0; i < size; i++)
            w[i] = 0.5f * (1f - Mathf.Cos(2f * Mathf.PI * i / (size - 1)));
        return w;
    }
}