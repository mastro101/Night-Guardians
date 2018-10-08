using UnityEngine;
using System.Collections;

public class PotenzaFazioni : MonoBehaviour
{
    public int[] potenzeFazioni = new int[6];

    public void AddPotenza(Fazioni fazione)
    {
        potenzeFazioni[(int)fazione] += 10;
    }

    public void AddPotenza()
    {
        foreach (int potenzaFazione in potenzeFazioni)
        {
            potenzeFazioni[potenzaFazione] += 10;
        }
    }

    public void RemovePotenza(Fazioni fazione)
    {
        potenzeFazioni[(int)fazione] -= 10;
    }

    public void RemovePotenza()
    {
        foreach (int potenzaFazione in potenzeFazioni)
        {
            potenzeFazioni[potenzaFazione] -= 10;
        }
    }

    public void ResetPotenza()
    {
        foreach (int potenzaFazione in potenzeFazioni)
        {
            potenzeFazioni[potenzaFazione] = 100;
        }
    }

    public int GetPotenza(Fazioni fazione)
    {
        return potenzeFazioni[(int)fazione];
    }

    public int GetPotenza()
    {
        int sumPotenze = 0;
        foreach (int potenzaFazione in potenzeFazioni)
        {
            sumPotenze += potenzaFazione;
        }
        return sumPotenze;
    }

    public float GetPercentPotenza(Fazioni fazioni)
    {
        return (100 / 6) * (potenzeFazioni[(int)fazioni] / 100);
    }

    public float GetPercentPotenza()
    {
        float sumProbability = 0;
        foreach (int potenzaFazione in potenzeFazioni)
        {
            sumProbability += (100 / 6) * (potenzaFazione / 100);
        }
        return sumProbability;
    }

    public float GetRangePotenza(Fazioni fazioni)
    {
        float range = 0;
        for (int i = 0; i < potenzeFazioni.Length; i++)
        {
            range += GetPercentPotenza((Fazioni)i);
            if (i == (int)fazioni)
                break;
        }
        return range;
    }
}
