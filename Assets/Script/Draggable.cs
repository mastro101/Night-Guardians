using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;

    CombatManager combatManager;
    Card card = null;
	GameObject placeholder = null;

    private void Awake()
    {
        combatManager = FindObjectOfType<CombatManager>();
        card = GetComponent<Card>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!combatManager.InCombat && card.Type != CardType.Nave)
        {
            Debug.Log("OnBeginDrag");

            placeholder = new GameObject();
            placeholder.transform.SetParent(this.transform.parent);
            LayoutElement le = placeholder.AddComponent<LayoutElement>();
            le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
            le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

            parentToReturnTo = this.transform.parent;
            placeholderParent = parentToReturnTo;
            this.transform.SetParent(this.transform.parent.parent);

            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
	}
	
	public void OnDrag(PointerEventData eventData) {
        //Debug.Log ("OnDrag");
        if (!combatManager.InCombat && card.Type != CardType.Nave)
        {
            this.transform.position = eventData.position;

            if (placeholder.transform.parent != placeholderParent)
                placeholder.transform.SetParent(placeholderParent);

            int newSiblingIndex = placeholderParent.childCount;

            for (int i = 0; i < placeholderParent.childCount; i++)
            {
                if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
                {

                    newSiblingIndex = i;

                    if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                        newSiblingIndex--;

                    break;
                }
            }

            placeholder.transform.SetSiblingIndex(newSiblingIndex);
        }
	}
	
	public void OnEndDrag(PointerEventData eventData) {
        if (!combatManager.InCombat && card.Type != CardType.Nave)
        {
            Debug.Log("OnEndDrag");
            this.transform.SetParent(parentToReturnTo);
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            DropZone dropZone = parentToReturnTo.GetComponent<DropZone>();

            if (dropZone != null)
            {
                switch (dropZone.Type)
                {
                    case DropZoneType.Hand:
                        card.positionCard = PositionCard.OnHand;
                        break;
                    case DropZoneType.Field:
                        card.positionCard = PositionCard.OnField;
                        break;
                    case DropZoneType.Support:
                        card.positionCard = PositionCard.OnField;
                        break;
                    default:
                        break;
                }
                if (dropZone.Type == DropZoneType.Field)
                {
                    // Suono quando viene messo in campo
                    card.PlaySound();
                    Debug.Log("BlaBlaBla");
                }
                if (card.Zone == DropZoneType.Support)
                    card.transform.rotation = Quaternion.Euler(0, 0, 90);
                else
                    card.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            Destroy(placeholder);
        }
	}
	
	
	
}
