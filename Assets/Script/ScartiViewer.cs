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
            card = Instantiate(cardObject, scartiViewerObject.transform);
            card.GetComponent<Card>().Data = scarti.Cards[i];
            card.transform.SetSiblingIndex(Random.Range(0, i + 1));
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
