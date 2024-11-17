using System.Collections;
using System;
using UnityEngine;

public class CurrentArrowManager : MonoBehaviour
{
    public static event Action<GameObject> OnNewCurrentArrow;
    public GameObject arrowsParent;
    private GameObject currentArrow;

    private void OnEnable()
    {
        ArrowProperties.OnArrowDestroy += HandleArrowDestroyed;
    }

    private void OnDisable()
    {
        ArrowProperties.OnArrowDestroy -= HandleArrowDestroyed;
    }

    private void Start()
    {
        StartCoroutine(MonitorArrows());
    }

    private IEnumerator MonitorArrows()
    {
        while (true)
        {
            if (currentArrow == null)
            {
                GameObject nextArrow = FindArrowReadyToBeCurrent();
                if (nextArrow != null)
                {
                    SetCurrentArrow(nextArrow);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void HandleArrowDestroyed()
    {
        currentArrow = null;
    }

    private void SetCurrentArrow(GameObject arrow)
    {
        currentArrow = arrow;
        currentArrow.GetComponent<ArrowProperties>().isCurrent = true;
        OnNewCurrentArrow?.Invoke(currentArrow);
    }

    // Поиск стрелки, готовой стать текущей (с флагом canBeCurrent)
    private GameObject FindArrowReadyToBeCurrent()
    {
        foreach (Transform child in arrowsParent.transform)
        {
            ArrowProperties arrowProps = child.GetComponent<ArrowProperties>();
            if (arrowProps != null && arrowProps.canBeCurrent && !arrowProps.isCurrent)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
