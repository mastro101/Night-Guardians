using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public DropZoneType Type;
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
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        Card cardDropped = eventData.pointerDrag.GetComponent<Card>();
        if (d != null)
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
}