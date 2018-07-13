using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public int childcount;

    public void Update()
    {
        childcount = gameObject.transform.childCount;
    }


    public void OnDrop(PointerEventData eventData) {
        Debug.Log("Ondrop to ");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Ondrop to ");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name); 
    }
}
