using UnityEngine;
using System;


public class RhythmEngine : MonoBehaviour
{
    [Range (0, 2)]
    public float timeScale;
    public int bpm = 120; // Биты в минуту
    public static float bitDelay; // Задержка между битами (с)
    public float timeToNextBit; // Время до следующего бита (с)
    public float onBitAccuracy = 0.1f; // Допустимая погрешность до бита в секундах

    public AudioSource audioSource; // Аудиоисточник для воспроизведения песни
    public AudioClip songClip; // Ссылка на песню
    public float songLength; // Длина песни в секундах

    private float bitTimer; // Внутренний таймер для отслеживания битов
    private float elapsedTime; // Общее время с начала игры

    // Статические переменные для доступа из других классов
    public static float accuracy; // Переменная акуратности (от -1 до +1)
    public float accuracy2;
    public static bool boolOnBit; // Попадание в тайминг бита

    // Ивенты для реакции на события
    public static event Action OnBitEvent; // Событие для каждого бита
    public static event Action OnSongEndEvent; // Событие окончания песни

    void Start()
    {
        bitDelay = 60f / bpm; // Рассчитываем задержку между битами на основе bpm
        bitTimer = 0; // Инициализация таймера

        if (songClip != null)
        {
            audioSource.clip = songClip; // Присваиваем аудиоклип аудиоисточнику
            songLength = songClip.length; // Получаем длину песни
            audioSource.Play(); // Запускаем воспроизведение песни
        }
        else
        {
            Debug.LogWarning("No song assigned to the AudioSource!");
        }

        // TriggerBit();
    }

    void Update()
    {
        //Time.timeScale = timeScale;
        float deltaTime = Time.deltaTime;
        bitTimer += deltaTime; // Увеличиваем таймер для битов
        elapsedTime += deltaTime; // Считаем общее время

        timeToNextBit = bitDelay - bitTimer; // Рассчитываем время до следующего бита
        // Нормализуем бит таймер в диапазоне от -1 до +1

        accuracy = Mathf.Abs((bitTimer / bitDelay) * 2f - 1f);

        accuracy2 = accuracy;

        // Проверяем, попал ли игрок в тайминг бита
        if (Mathf.Abs(accuracy) >= 1 - onBitAccuracy)
        {
            boolOnBit = true;
        }
        else
        {
            boolOnBit = false;
        }

        // Если время для следующего бита наступило
        if (bitTimer >= bitDelay)
        {
            TriggerBit();
            bitTimer -= bitDelay; // Сбрасываем таймер с учётом возможного проскока
        }

        // Проверяем, если песня завершилась
        if (audioSource != null && !audioSource.isPlaying && elapsedTime >= songLength)
        {
            TriggerSongEnd();
        }
    }

    // Метод, который срабатывает при наступлении бита
    void TriggerBit()
    {   
        // Debug.Log("Bit Triggered! Time: " + elapsedTime);
        OnBitEvent?.Invoke(); // Вызов события для реакции на бит
    }

    // Метод, который срабатывает при завершении песни
    void TriggerSongEnd()
    {
        Debug.Log("Song Ended!");
        OnSongEndEvent?.Invoke(); // Вызов события окончания песни
    }
}
