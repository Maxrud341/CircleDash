using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroying : MonoBehaviour
{
    public void DestroySelf(){
        Destroy(gameObject);
    }
}
