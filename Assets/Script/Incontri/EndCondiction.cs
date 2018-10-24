using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class EndCondiction : MonoBehaviour
{
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    TextMeshProUGUI text;
    bool end;

    public void EndGame(bool _win)
    {
        if (!end)
        {
            endScreen.SetActive(true);
            if (_win)
            {
                FindObjectOfType<PotenzaFazioni>().AddPotenza();
                text.text = "You Win";
            }
            else
                text.text = "You Lose";
            end = true;
        }
    }

    private void Update()
    {
        if (end)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }
}
