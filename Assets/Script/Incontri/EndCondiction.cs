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
    [HideInInspector]
    public bool InEnd;
    [HideInInspector]
    public bool Ended;
    CardsOfDeck cardsOfDeck;
    Deck deck;
    Scarti scarti;
    int map = 1;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Ended = false;
        cardsOfDeck = FindObjectOfType<CardsOfDeck>();
        deck = FindObjectOfType<Deck>();
        scarti = FindObjectOfType<Scarti>();
    }

    public void EndGame(bool _win)
    {
        if (!Ended)
        {
            Ended = true;
            endScreen.SetActive(true);
            if (_win)
            {
                FindObjectOfType<PotenzaFazioni>().AddPotenza();
                levelManager.LevelMap++;
                text.text = "You Win";
                cardsOfDeck.Cards = new CardsData[50];
                cardsOfDeck.FillDeck(deck.Cards);
                cardsOfDeck.FillDeck(scarti.Cards);
                if (levelManager.FinalLevel)
                    levelManager.LevelMap = 1;
                if (levelManager.FinalLevel && levelManager.MapSceneName == "MapStage3")
                {

                }
                else
                {
                    if (!levelManager.FinalLevel)
                        SceneManager.LoadScene(levelManager.MapSceneName);
                    else
                    {
                        map++;
                        levelManager.MapSceneName = "MapStage" + map.ToString();
                        SceneManager.LoadScene(levelManager.MapSceneName);
                        levelManager.FinalLevel = false;
                    }
                }
            }
            else
                text.text = "You Lose";
        }
    }

    private void Update()
    {
        if (Ended)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
