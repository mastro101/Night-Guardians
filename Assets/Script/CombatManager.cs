using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    DropZone zoneField, zoneEnemy, zoneSupport;
    public Card[] cardInField;
    public Card Enemy;
    public Card Support;
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
        if (zoneEnemy.transform.childCount != 0)
            Enemy = zoneEnemy.transform.GetChild(0).GetComponent<Card>();
    }

    public void ButtonCombat()
    {
        if (zoneEnemy.transform.childCount != 0 && zoneField.transform.childCount != 0)
        Combat();
    }

    public void Combat()
    {
        InCombat = true;
        Debug.Log("MORTAL COMBAAAAAAT");
        if (zoneSupport.transform.childCount != 0)
            Support = zoneSupport.transform.GetChild(0).gameObject.GetComponent<Card>();
        Enemy = zoneEnemy.transform.GetChild(0).GetComponent<Card>();
        numberOfCardInField = zoneField.transform.childCount;
        // Prendo i componenti delle carte in campo 
        for (int i = 0; i < numberOfCardInField; i++)
        {
            cardInField[i] = zoneField.transform.GetChild(i).gameObject.GetComponent<Card>();
            if (Support != null)
            {
                switch (Support.Data.Supporto)
                {
                    case Buff.Attack:
                        cardInField[i].Attack++;
                        break;
                    case Buff.Life:
                        cardInField[i].Life++;
                        break;
                    case Buff.Purification:
                        cardInField[i].PurificationOrDarkness++;
                        break;
                    default:
                        Debug.Log("No buff");
                        break;
                }
            }
        }

        if (Enemy != null)
        {
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
        }
        else
        {
            Debug.Log("Non ci sono nemici");
        }

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
        {
            InCombat = false;
            Destroy(Enemy.gameObject);
        }
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