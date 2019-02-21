using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager levelManagerPointer;

    [HideInInspector]
    public int LevelIncontro;
    public int LevelMap;
	[HideInInspector] public string MapSceneName = "MapStage";
    public bool SpecialLevel;

	private void Awake()
    {
		if(levelManagerPointer == null)
			levelManagerPointer = this;
	
        if (LevelMap == 0)
            LevelMap = 1;
    }

	

}
