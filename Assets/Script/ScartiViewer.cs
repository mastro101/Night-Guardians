using UnityEngine;
using System.Collections;

public class ScartiViewer : MonoBehaviour
{
    [SerializeField]
    GameObject scartiViewerObject;
    Scarti scarti;
    [SerializeField]
    GameObject cardObject;
    GameObject card;
    

    private void Awake()
    {
        scarti = FindObjectOfType<Scarti>();
    }

    public void ViewScarti()
    {
        for (int i = 0; i < scarti.ScartedCard; i++)
        {
            card = Instantiate(scarti.playableCardInScartiTR.GetChild(i).gameObject, scartiViewerObject.transform);
            card.transform.SetSiblingIndex(Random.Range(0, i + 1));
            Destroy(card.GetComponent<Draggable>());
        }
    }

    public void Close()
    {
        for (int i = 0; i < scarti.ScartedCard; i++)
        {
            Destroy(scartiViewerObject.transform.GetChild(i).gameObject);
        }
        scartiViewerObject.SetActive(false);
    }
}
