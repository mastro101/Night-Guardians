using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class ContenitoreCards : MonoBehaviour {

    EnemySpawn enemySpawn;
    public CardsData[] Cards;
    //[SerializeField] CardsData[] EnemyCards;
    CardsData card;
    PotenzaFazioni potenzaFazioni;
    LevelManager levelManager;


	[Space, Header("Enemy Ships"), SerializeField]
	List<EnemyFactionCards> enemyFactions;

	//levelManager.unlockedTier3
	//private bool unlockedLevel3 = false;


    bool IsPossibleFazione = true;
    int SumGradoEnemy;




    private void Awake()
    {
        if (FindObjectOfType<EnemySpawn>())
        {
            enemySpawn = FindObjectOfType<EnemySpawn>();
        }
        if (FindObjectOfType<PotenzaFazioni>())
        {
            potenzaFazioni = FindObjectOfType<PotenzaFazioni>();
        }
        if (FindObjectOfType<LevelManager>())
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
    }

    private void Start()
    {
        //fillEnemyDeck();	//old

		RemoveGarbageFaction();
		fillEnemyDeckV2();
	}

	/// <summary>
	/// rimuove eventuali doppioni in enemyFactions,
	/// rimane solo il primo elemento trovato per ogni fazione
	/// </summary>
	private void RemoveGarbageFaction() {
		foreach(Fazioni var in System.Enum.GetValues(typeof(Fazioni)))
		{
			bool find = false;
			for(int i = 0; i < enemyFactions.Count; i++)
			{
				if(var == enemyFactions[i].faction) 
				{
					if(find == true) 
					{
						enemyFactions.RemoveAt(i);
						i--;
					} 
					else 
					{
						find = true;
					}
				}
			}
			find = false;
		}
	}


    /*private void fillEnemyDeck()
    {
		//old code
        if (enemySpawn != null)
        {
            enemySpawn.EnemyLeft = 0;
            for (SumGradoEnemy = 0; SumGradoEnemy < levelManager.LevelIncontro;)
            {
                do
                {
                    IsPossibleFazione = false;
                    card = EnemyCards[Random.Range(0, EnemyCards.Length)];
                    if (card != null)
                    {
                        foreach (Fazioni fazione in potenzaFazioni.PossibiliFazioniDaIncontrare)
                        {
                            if (card.Fazione == fazione)
                            {
                                IsPossibleFazione = true;
                                break;
                            }
                        }
                        if (card.Grado + SumGradoEnemy == levelManager.LevelIncontro && IsPossibleFazione == true)
                            break;
                    }
                }
                while (!IsPossibleFazione || SumGradoEnemy + card.Grado > levelManager.LevelIncontro);
                SumGradoEnemy += card.Grado;
                enemySpawn.FillEnemyCards(card);
            }
            SumGradoEnemy = 0;
        }
    }
	*/

	private void fillEnemyDeckV2() {
		// new code
		int maxPowerLevel = potenzaFazioni.unlockedTier3 ? 3 : 2;

		if (enemySpawn != null)
		{
			ShipCalcPower encounterPower = new ShipCalcPower();

			CalculateEncounterDistribution(encounterPower, maxPowerLevel);			
			ShuffleShipsOrder(encounterPower);

			List<CardsData> enemyShips = new List<CardsData>();
			
			for (int i = 0; i < encounterPower.shipPower.Count; i++)
			{
				Fazioni actFaction = potenzaFazioni.PossibiliFazioniDaIncontrare[Random.Range(0, potenzaFazioni.PossibiliFazioniDaIncontrare.Length)];
				List<ShipsList> enemyShipsDb = GetShipsListFromFaction(actFaction);
				List<CardsData> rightLevelShipsList = enemyShipsDb[encounterPower.shipPower[i]].Ships;
				CardsData actEnemyShip = rightLevelShipsList[Random.Range(0, rightLevelShipsList.Count)];
				enemyShips.Add(actEnemyShip);
			}

			enemySpawn.FillEnemyCards(enemyShips);

			/*string debug = "";
			foreach(int var in encounterPower.shipPower) {
				debug += var + " ";
			}
			Debug.LogWarning("power required: " + levelManager.LevelIncontro + "   :   " + debug);*/
		}
	}

	private void CalculateEncounterDistribution(ShipCalcPower encounterPower, int maxPowerLevel) 
	{
		int randomRangeMax = maxPowerLevel;
		while (randomRangeMax > 1)
		{
			randomRangeMax = 1 + Mathf.Min(maxPowerLevel, levelManager.LevelIncontro - encounterPower.GetTotalEncounterLevel());
			if (randomRangeMax > 1)
			{
				encounterPower.shipPower.Add(Random.Range(1, randomRangeMax));
			}
		}
	}

	private void ShuffleShipsOrder(ShipCalcPower encounterPower) 
	{
		for(int i = 0; i < encounterPower.shipPower.Count; i++) 
		{
			int randomRange = Random.Range(0, encounterPower.shipPower.Count);
			int tmp = encounterPower.shipPower[i];
			encounterPower.shipPower[i] = encounterPower.shipPower[randomRange];
			encounterPower.shipPower[randomRange] = tmp;
		}
	}


	public CardsData FindCard(string _name)
    {
        foreach (CardsData card in Cards)
        {
            if (card.Name == _name)
            {
                return card;
            }
        }
        return null;
    }

	public List<ShipsList> GetShipsListFromFaction(Fazioni requiredFaction) 
	{
		foreach(EnemyFactionCards tmpFaction in enemyFactions)
		{
			if(tmpFaction.faction == requiredFaction) 
			{
				return tmpFaction.level;
			}
		}

		return null;
	}

}


[System.Serializable]
public class EnemyFactionCards {
	public Fazioni faction;
	public List<ShipsList> level;
}

[System.Serializable]
public struct ShipsList {
	public List<CardsData> Ships;
}

public class ShipCalcPower {
	public List<int> shipPower = new List<int>();

	public int GetTotalEncounterLevel() {
		int sum = 0;
		foreach(int var in shipPower) {
			sum += var;
		}
		return sum;
	}

}