using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoad : MonoBehaviour
{
    public int mapIndex;    
    public void loadMap(){
        SceneManager.LoadScene(mapIndex);
    }
}
