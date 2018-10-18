using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour {

    public Fazioni[] possibiliFazioni;
    public Text TextFazioni;
    PotenzaFazioni potenzaFazioni;
    LevelManager levelManager;

    private void Awake()
    {
        possibiliFazioni = new Fazioni[Random.Range(2, 5)];
        TextFazioni = transform.GetChild(0).GetComponent<Text>();
        potenzaFazioni = FindObjectOfType<PotenzaFazioni>();
        levelManager = FindObjectOfType<LevelManager>();
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
        potenzaFazioni.PossibiliFazioniDaIncontrare = new Fazioni[possibiliFazioni.Length];
        for (int i = 0; i < possibiliFazioni.Length; i++)
        {
            potenzaFazioni.PossibiliFazioniDaIncontrare[i] = possibiliFazioni[i];
        }
        switch (SceneManager.GetActiveScene().name)
        {
            case "MapStage1":
                levelManager.LevelIncontro = Random.Range(3, 6);
                break;
            case "MapStage2":
                levelManager.LevelIncontro = Random.Range(5, 8);
                break;
            case "MapStage3":
                levelManager.LevelIncontro = Random.Range(7, 10);
                break;
            default:
                break;
        }
        
        SceneManager.LoadScene("Incontro");
    }
}
