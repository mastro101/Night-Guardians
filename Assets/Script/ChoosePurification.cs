using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ChoosePurification : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public Card OriginalCard;
    Transform parentGO;

    private void Start()
    {
        parentGO = transform.parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OriginalCard.Data = GetComponent<Card>().Data;
        for (int i = 0; i < parentGO.childCount; i++)
        {
            Destroy(parentGO.GetChild(i).gameObject);
        }
        parentGO.gameObject.SetActive(false);
    }
}
