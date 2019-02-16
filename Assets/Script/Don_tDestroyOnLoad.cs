using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Don_tDestroyOnLoad : MonoBehaviour
{
    static bool alreadyExists = false;

    void Awake()
    {
        if (alreadyExists)
            Destroy(this.gameObject);
        else
		{ 
            DontDestroyOnLoad(gameObject);
            alreadyExists = true;
        }
    }

	private void Update()
	{
		if (SceneManager.GetActiveScene().name == "MainMenu") {
			alreadyExists = false;
			Destroy(this.gameObject);
		}
	}

}
