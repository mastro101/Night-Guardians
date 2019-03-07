using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EvolveCard : MonoBehaviour , IPointerDownHandler
{
    [HideInInspector]
    public Card OriginalCard;
    Transform parentGO;
    EndCondiction endCondiction;
	CombatManager combatManager;

    private void Awake()
    {
        endCondiction = FindObjectOfType<EndCondiction>();
		combatManager = FindObjectOfType<CombatManager>();

	}

    private void Start()
    {
        parentGO = transform.parent;
    }

    void evolveCard()
    {
        OriginalCard.Evolve();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        evolveCard();
        for (int i = 0; i < parentGO.childCount; i++)
        {
            Destroy(parentGO.GetChild(i).gameObject);
        }
		endCondiction.endSelect = true;
		parentGO.parent.gameObject.SetActive(false);
		combatManager.DestroyOldEnemy();

	}
}
