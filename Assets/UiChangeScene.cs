using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiChangeScene : MonoBehaviour {

	public void ChangeScene(string targetSceneName) {
		SceneManager.LoadScene(targetSceneName);
	}



}
