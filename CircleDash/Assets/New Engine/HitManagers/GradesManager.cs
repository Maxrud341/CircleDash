using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradesManager : MonoBehaviour
{
    [SerializeField] private GameObject Grades;
    [SerializeField] private GameObject PerfectGrade;
    [SerializeField] private GameObject GoodGrade;
    [SerializeField] private GameObject MissGrade;

    public void CreatePerfectGrade()
    {
        CreateGrade(PerfectGrade);
    }

    public void CreateGoodGrade()
    {
        CreateGrade(GoodGrade);
    }

    public void CreateMissGrade()
    {
        CreateGrade(MissGrade);
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
