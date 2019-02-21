using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public DropZoneType Type;
    public int CardLimit;
    CombatManager combatManager;

    private void Awake()
    {
        combatManager = FindObjectOfType<CombatManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeholderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
        d.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        Card cardDropped = d.GetComponent<Card>();
        Debug.Log(cardDropped.Data.Name + " was dropped on " + gameObject.name);

        if (d != null && transform.childCount <= CardLimit)
        {
            d.parentToReturnTo = this.transform;
            if (cardDropped != null && !combatManager.InCombat)
            {
                cardDropped.Zone = Type;
            }
        }
    }
}

public enum DropZoneType
{
    Hand,
    Enemy,
    Field,
    Support,
}