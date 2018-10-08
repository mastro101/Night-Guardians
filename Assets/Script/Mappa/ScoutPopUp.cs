using UnityEngine;
using System.Collections;

public class ScoutPopUp : MonoBehaviour
{
    [SerializeField]
    GameObject PopUpLevel;
    bool WasOpen;
    [SerializeField]
    SelectLevel[] incontri;
    PotenzaFazioni potenzaFazioni;

    Fazioni fazione;
    Fazioni[] fazioni;

    private void Awake()
    {
        potenzaFazioni = FindObjectOfType<PotenzaFazioni>();
    }

    private void Start()
    {
        WasOpen = false;
    }

    public void OpenPopUp()
    {
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
                else if (IntRandomFazione < potenzaFazioni.GetRangePotenza(Fazioni.PiratiVeri))
                    fazione = Fazioni.PiratiVeri;
                else if (IntRandomFazione < potenzaFazioni.GetRangePotenza(Fazioni.Marina))
                    fazione = Fazioni.Marina;
                else if (IntRandomFazione < potenzaFazioni.GetRangePotenza(Fazioni.Voodoo))
                    fazione = Fazioni.Voodoo;
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
