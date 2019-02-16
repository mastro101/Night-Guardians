using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

	public GameObject map2;
	public GameObject map3;


	void Start()
	{
		if (!LevelManager.levelManagerPointer.unlockMap2 && LevelManager.levelManagerPointer.LevelMap > 6)
		{
			//Debug.Log("Map 2 Unlocked");
			map2.SetActive(true);
			LevelManager.levelManagerPointer.unlockMap2 = true;
		}
		if (!LevelManager.levelManagerPointer.unlockMap3 && LevelManager.levelManagerPointer.LevelMap > 12)
		{
			//Debug.Log("Map 3 Unlocked");
			map3.SetActive(true);
			LevelManager.levelManagerPointer.unlockMap3 = true;
		}
	}


}
