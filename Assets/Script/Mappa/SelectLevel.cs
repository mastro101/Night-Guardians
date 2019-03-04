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
        possibiliFazioni = new Fazioni[Random.Range(2, 4)];
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
        switch (levelManager.LevelMap)
        {
            case 1:                                     //primo incontro
				levelManager.LevelIncontro = 3;
				break;

			case 2:
			case 3:
			case 4:
			case 5:
			case 6:										//tutti gli incontri escluso il primo della prima mappa
                levelManager.LevelIncontro = Random.Range(3, 6);
                break;

            case 7:                                     //primo incontro della seconda mappa
				potenzaFazioni.unlockedTier3 = true;
                levelManager.LevelIncontro = 5;
				break;

			case 8:
			case 9:
			case 10:
			case 11:
			case 12:                                    //tutti gli incontri escluso il primo della seconda mappa
				levelManager.LevelIncontro = Random.Range(5, 8);
                break;

            case 13:                                    //primo incontro della terza mappa
				levelManager.LevelIncontro = 7;
				break;

			case 14:
			case 15:
			case 16:
			case 17:
			case 18:									//tutti gli incontri escluso il primo della terza mappa
				levelManager.LevelIncontro = Random.Range(7, 10);
                break;

            default:
                break;
        }
        
        SceneManager.LoadScene("Incontro");
    }
}
