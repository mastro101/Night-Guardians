using UnityEngine;
using System.Collections;

public class PotenzaFazioni : MonoBehaviour
{
    private static bool created = false;

    public int[] PotenzeFazioni = new int[6];
    public Fazioni[] PossibiliFazioniDaIncontrare;
	public bool unlockedTier3 = false;

    public void AddPotenza(Fazioni fazione)
    {
        PotenzeFazioni[(int)fazione] += 10;
    }

    public void AddPotenza()
    {
        for (int i = 0; i < PotenzeFazioni.Length; i++)
        {
            PotenzeFazioni[i] += 10;
        }
    }

    public void RemovePotenza(Fazioni fazione, int grado)
    {
        PotenzeFazioni[(int)fazione] -= 10 * grado;
    }

    public void RemovePotenza()
    {
        for (int i = 0; i < PotenzeFazioni.Length; i++)
        {
            PotenzeFazioni[i] -= 10;
        }
    }

    public void ResetPotenza()
    {
        for (int i = 0; i < PotenzeFazioni.Length; i++)
        {
            PotenzeFazioni[i] = 100;
        }
    }

    public int GetPotenza(Fazioni fazione)
    {
        return PotenzeFazioni[(int)fazione];
    }

    public int GetPotenza()
    {
        int sumPotenze = 0;
        foreach (int potenzaFazione in PotenzeFazioni)
        {
            sumPotenze += potenzaFazione;
        }
        return sumPotenze;
    }

    public float GetPercentPotenza(Fazioni fazioni)
    {
        return (100 / 6) * (PotenzeFazioni[(int)fazioni] / 100);
    }

    public float GetPercentPotenza()
    {
        float sumProbability = 0;
        foreach (int potenzaFazione in PotenzeFazioni)
        {
            sumProbability += (100 / 6) * (potenzaFazione / 100);
        }
        return sumProbability;
    }

    public float GetRangePotenza(Fazioni fazioni)
    {
        float range = 0;
        for (int i = 0; i < PotenzeFazioni.Length; i++)
        {
            range += GetPercentPotenza((Fazioni)i);
            if (i == (int)fazioni)
                break;
        }
        return range;
    }

}
