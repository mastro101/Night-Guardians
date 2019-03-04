using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoutPopUp : MonoBehaviour
{
    [SerializeField]
    GameObject PopUpLevel;
    [SerializeField]
    bool finalLevel;
    bool WasOpen;
    [SerializeField]
    int LevelMap;
    [SerializeField]
    SelectLevel[] incontri;
    PotenzaFazioni potenzaFazioni;
    LevelManager levelManager;

	[SerializeField] private Color disabledColor;
	[SerializeField] private Image completed;

    Fazioni fazione;
    Fazioni[] fazioni;

    private void Awake()
    {
        potenzaFazioni = FindObjectOfType<PotenzaFazioni>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        WasOpen = false;
        if (LevelMap != levelManager.LevelMap) {
            Destroy(GetComponent<Button>());
			Image pointer = GetComponent<Image>();
			pointer.raycastTarget = false;
			if(LevelMap < levelManager.LevelMap) {
				if(completed != null) {
					completed.gameObject.SetActive(true);
				}
			}
			else {
				pointer.color = disabledColor;
			}
		}
    }

    public void OpenPopUp()
    {
        if (!WasOpen)
        {
            if (finalLevel)
                levelManager.SpecialLevel = true;
            else
                levelManager.SpecialLevel = false;
            WasOpen = true;
            PopUpLevel.SetActive(true);
            for (int i = 0; i < incontri.Length; i++)
            {
                incontri[i] = PopUpLevel.transform.GetChild(i).GetComponent<SelectLevel>();
                fazioni = new Fazioni[incontri[i].possibiliFazioni.Length];

                for (int n = 0; n < incontri[i].possibiliFazioni.Length; n++)
                {
                    float IntRandomFazione = Random.Range(0f, potenzaFazioni.GetPercentPotenza());

                    if (IntRandomFazione < potenzaFazioni.GetRangePotenza(Fazioni.NonMorti))
                        fazione = Fazioni.NonMorti;
                    else if (IntRandomFazione < potenzaFazioni.GetRangePotenza(Fazioni.Orientali))
                        fazione = Fazioni.Orientali;
                    //else if (IntRandomFazione < potenzaFazioni.GetRangePotenza(Fazioni.PiratiVeri))
                      //  fazione = Fazioni.PiratiVeri;
                    else if (IntRandomFazione < potenzaFazioni.GetRangePotenza(Fazioni.Marina))
                        fazione = Fazioni.Marina;
                    //else if (IntRandomFazione < potenzaFazioni.GetRangePotenza(Fazioni.Voodoo))
                      //  fazione = Fazioni.Voodoo;
                    else if (IntRandomFazione < potenzaFazioni.GetRangePotenza(Fazioni.Kraken))
                        fazione = Fazioni.Kraken;

                    incontri[i].possibiliFazioni[n] = fazione;
                    fazioni[n] = fazione;
                    for (int m = 0; m < fazioni.Length; m++)
                    {
                        if (n == m)
                            break;

                        if (fazioni[n] == fazioni[m])
                        {
                            n--;
                            break;
                        }
                    }
                }

                incontri[i].WriteTextFazioni();

            }
        }
    }
}
