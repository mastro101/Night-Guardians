using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class handzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public int childnumber;

    public void Update ()

    {

        childnumber = transform.childCount;

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        if (eventData.pointerDrag == null)
            return;

        cardsbeh d = eventData.pointerDrag.GetComponent<cardsbeh>();
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

        cardsbeh d = eventData.pointerDrag.GetComponent<cardsbeh>();
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        cardsbeh d = eventData.pointerDrag.GetComponent<cardsbeh>();
        if (d != null)
        {
            d.parentToReturnTo = this.transform;
        }

    }

    
}
