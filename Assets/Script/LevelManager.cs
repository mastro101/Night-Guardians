using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public int LevelIncontro;
    public int LevelMap;
    public string MapSceneName;

    private void Awake()
    {
        if (LevelMap == 0)
            LevelMap = 1;
        if (SceneManager.GetActiveScene().name == "MapStage1" || SceneManager.GetActiveScene().name == "MapStage2" || SceneManager.GetActiveScene().name == "MapStage3")
        {
            MapSceneName = SceneManager.GetActiveScene().name;
        }
    }
}
