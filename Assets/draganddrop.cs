using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class draganddrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    Transform ParentToReturnTo = null;

    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");

        ParentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");

        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");
        this.transform.SetParent(ParentToReturnTo);

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}


