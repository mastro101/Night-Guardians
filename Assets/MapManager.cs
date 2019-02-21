using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour {

	public GameObject map2;
	public GameObject map3;

	bool unlockMap2 = false;
	bool unlockMap3 = false;

	void Start()
	{
		if (!unlockMap2 && LevelManager.levelManagerPointer.LevelMap > 6)
		{
			map2.SetActive(true);
			unlockMap2 = true;
		}
		if (!unlockMap3 && LevelManager.levelManagerPointer.LevelMap > 12)
		{
			map3.SetActive(true);
			unlockMap3 = true;
		}
	}


}
