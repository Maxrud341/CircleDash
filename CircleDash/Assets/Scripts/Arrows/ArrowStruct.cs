using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStruct : MonoBehaviour
{
    
}

    [System.Serializable]
    public struct Arrow{
        public GameObject arrow;
        public int derection;
        public float delay;
    }

    [System.Serializable]
    public struct ArrowLevel{
        public Arrow[] arrows;
    }