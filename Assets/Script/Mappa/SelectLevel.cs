using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour {

    public Scene LevelScene;
    public Fazioni[] possibiliFazioni;
    public Text TextFazioni;

    private void Awake()
    {
        possibiliFazioni = new Fazioni[Random.Range(2, 5)];
        TextFazioni = transform.GetChild(0).GetComponent<Text>();
    }

    public void WriteTextFazioni()
    {
        TextFazioni.text = "";
        foreach (Fazioni fazione in possibiliFazioni)
        {
            TextFazioni.text += (fazione.ToString() + " ");
        }
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(LevelScene.ToString());
    }
}
