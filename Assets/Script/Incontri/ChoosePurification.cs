using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ChoosePurification : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public Card OriginalCard;
    Transform parentGO;
    CloseWindow closeWindow;
    EndCondiction endCondiction;
	CombatManager combatManager;
    Deck deck;

    private void Awake()
    {
        deck = FindObjectOfType<Deck>();
        parentGO = transform.parent;
        closeWindow = transform.parent.parent.GetChild(1).GetComponent<CloseWindow>();
        endCondiction = FindObjectOfType<EndCondiction>();
		combatManager = FindObjectOfType<CombatManager>();
	}

    private void Start()
    {
        closeWindow.OnClose += destroyOriginalCard;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OriginalCard.Data = GetComponent<Card>().Data;
        deck.FillDeck(OriginalCard.Data);
        for (int i = 0; i < parentGO.childCount; i++)
        {
            Destroy(parentGO.GetChild(i).gameObject);
        }
		endCondiction.endSelect = true;
        parentGO.parent.gameObject.SetActive(false);
		combatManager.DestroyOldEnemy();
	}

    void destroyOriginalCard()
    {
        if (OriginalCard != null)
            Destroy(OriginalCard.gameObject);
    }
}
