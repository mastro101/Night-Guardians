using UnityEngine;
using System.Collections;
using TMPro;

public class Resources : MonoBehaviour
{

    int Coin;
    [SerializeField]
    TextMeshProUGUI CoinText;

    public void AddCoin(int _coin)
    {
        Coin += _coin;
        CoinText.text = " " + Coin.ToString();
    }
}
