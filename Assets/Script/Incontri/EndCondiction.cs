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
    LevelManager levelManager;
    bool end;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Debug.Log(levelManager.MapSceneName);
        end = false;
    }

    public void EndGame(bool _win)
    {
        if (!end)
        {
            end = true;
            endScreen.SetActive(true);
            if (_win)
            {
                FindObjectOfType<PotenzaFazioni>().AddPotenza();
                levelManager.LevelMap++;
                text.text = "You Win";
                SceneManager.LoadScene(levelManager.MapSceneName);
            }
            else
                text.text = "You Lose";
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
