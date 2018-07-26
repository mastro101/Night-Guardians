using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour {

    [SerializeField]
    GameObject quitMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!quitMenu.activeInHierarchy)
                quitMenu.SetActive(true);
            else
                quitMenu.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CloseMenu()
    {
        quitMenu.SetActive(false);
    }
}
