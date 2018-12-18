using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {

    [SerializeField]
    GameObject quitMenuGO;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (quitMenuGO)
            {
                if (!quitMenuGO.activeInHierarchy)
                    quitMenuGO.SetActive(true);
                else
                    quitMenuGO.SetActive(false);
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CloseMenu()
    {
        if (quitMenuGO)
        {
            if (!quitMenuGO.activeInHierarchy)
                quitMenuGO.SetActive(true);
            else
                quitMenuGO.SetActive(false);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
