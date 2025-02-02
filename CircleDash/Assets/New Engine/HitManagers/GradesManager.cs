using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradesManager : MonoBehaviour
{
    public int PerfectGradeCount = 0;
    public int GoodGradeCount = 0;
    public int MissGradeCount = 0;


    [SerializeField] private GameObject Grades;
    [SerializeField] private GameObject PerfectGrade;
    [SerializeField] private GameObject GoodGrade;
    [SerializeField] private GameObject MissGrade;


    public void CreatePerfectGrade()
    {
        CreateGrade(PerfectGrade);
        PerfectGradeCount++;
        Debug.Log(PerfectGradeCount);
    }

    public void CreateGoodGrade()
    {
        CreateGrade(GoodGrade);
        GoodGradeCount++;
    }

    public void CreateMissGrade()
    {
        CreateGrade(MissGrade);
        MissGradeCount++;
    }

    private void CreateGrade(GameObject gradePrefab)
    {
        if (gradePrefab != null && Grades != null)
        {
            GameObject newGrade = Instantiate(gradePrefab, Grades.transform.position, Quaternion.identity, Grades.transform);
        }
        else
        {
            Debug.LogWarning("Grade prefab or Grades object is not assigned.");
        }
    }
}
