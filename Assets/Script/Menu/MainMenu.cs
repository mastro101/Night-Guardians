﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Update () {
		
        if (Input.anyKey || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MapStage1");
        }
	}
}
