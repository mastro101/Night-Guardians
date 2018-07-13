using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class attackzones : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public int childnumber;
    public bool ciao;

    


    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        if (eventData.pointerDrag == null)
            return;

        if (childnumber >= 1)
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

        if (childnumber >= 1)
            return;


        cardsbeh d = eventData.pointerDrag.GetComponent<cardsbeh>();
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (ciao == true)
            return;
 

        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        cardsbeh d = eventData.pointerDrag.GetComponent<cardsbeh>();
        if (d != null)
        {
            d.parentToReturnTo = this.transform;
        }

    }

    public void LateUpdate()

    {

        childnumber = transform.childCount;
        if (childnumber == 0 && ciao == true)

            ciao = false;
    }

}
