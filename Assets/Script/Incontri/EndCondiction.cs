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
    public bool Ended;
    CardsOfDeck cardsOfDeck;
    Deck deck;
    Scarti scarti;
	public bool winEncounter = false;
	public bool winGame = false;

	public bool endSelect = false;

	CombatManager combatManager;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Ended = false;
		cardsOfDeck = FindObjectOfType<CardsOfDeck>();
        deck = FindObjectOfType<Deck>();
        scarti = FindObjectOfType<Scarti>();
		combatManager = FindObjectOfType<CombatManager>();

	}

    public void EndGame(bool _win)
    {
		winEncounter = _win;
		if (!Ended)
        {
            Ended = true;
			if(!winEncounter)
				endScreen.SetActive(true);

            if (winEncounter)
            {
                FindObjectOfType<PotenzaFazioni>().AddPotenza();
                levelManager.LevelMap++;
                cardsOfDeck.Cards = new CardsData[50];
                cardsOfDeck.FillDeck(deck.Cards);
                cardsOfDeck.FillDeck(scarti.Cards);
                if (levelManager.SpecialLevel && levelManager.LevelMap >= 18)
                {
					winGame = true;
                    Debug.Log("Fine");
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
			if(endSelect) 
			{
				SceneManager.LoadScene(levelManager.MapSceneName);
			}

			if (Input.GetKeyDown(KeyCode.Escape))
            {
				if (!winEncounter)
				{
					SceneManager.LoadScene("MainMenu");
				}
            }
        }
		if(endSelect) {
			endSelect = false;
		}

    }
}
