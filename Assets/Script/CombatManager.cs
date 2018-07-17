using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    DropZone zoneField, zoneEnemy;
    public Card[] cardInField;
    public Card Enemy;
    [HideInInspector]
    public bool InCombat = false;
    int numberOfCardInField;
    int CardDestroied;

    private void Awake()
    {
        cardInField = new Card[3];
    }

    private void Start()
    {
        if (zoneEnemy.transform.GetChild(0) != null)
            Enemy = zoneEnemy.transform.GetChild(0).GetComponent<Card>();
    }

    public void Combat()
    {
        InCombat = true;
        Debug.Log("MORTAL COMBAAAAAAT");

        numberOfCardInField = zoneField.transform.childCount;
        // Prendo i componenti delle carte in campo 
        for (int i = 0; i < numberOfCardInField; i++)
        {
            cardInField[i] = zoneField.transform.GetChild(i).gameObject.GetComponent<Card>();
        }

        do
        {    
            CardDestroied = 3 - numberOfCardInField;
            for (int i = 0; i < 3; i++)
            {
                if (cardInField[i] != null)
                {
                    if (Enemy.IsAlive)
                    {
                        Attack(i);
                        Purifica(i);
                    }
                }
            }
        } while (InCombat && CardDestroied < 3);
        InCombat = false;
    }

    void Attack(int _cardInField)
    {
        Enemy.Life -= cardInField[_cardInField].Attack;
        cardInField[_cardInField].Life -= Enemy.Attack;
        CheckLifeAndDestroy(_cardInField);
    }

    void CheckLifeAndDestroy(int _cardInField)
    {
        if (!Enemy.IsAlive)
            InCombat = false;
        if (!cardInField[_cardInField].IsAlive)
        {
            Destroy(cardInField[_cardInField].gameObject);
            CardDestroied++;
        }
    }

    void Purifica (int _cardInField)
    {
        if (Enemy.IsAlive)
            Enemy.PurificationOrDarkness -= cardInField[_cardInField].PurificationOrDarkness;
    }
}
