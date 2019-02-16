using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CloseWindow : MonoBehaviour
{
    Transform CardsInWindow;
	EndCondiction endCondiction;

    public event CloseWindowEvent.CloseWindowDelegate OnClose;

	public void Awake()
	{
		endCondiction = FindObjectOfType<EndCondiction>();
	}

	public void CloseTheWindow()
    {
        InvockOnClose();
        DestroyCardsClone();
        transform.parent.gameObject.SetActive(false);
    }

    private void DestroyCardsClone()
    {
        CardsInWindow = transform.parent.GetChild(0);
        for (int i = 0; i < CardsInWindow.childCount; i++)
        {
            Destroy(CardsInWindow.GetChild(i).gameObject);
        }
    }

	public void BackToMap() {
		endCondiction.EndGame(true);
	}

    #region Event

    void InvockOnClose()
    {
        if (OnClose!= null)
            OnClose();
    }

    #endregion
}

public class CloseWindowEvent
{
    public delegate void CloseWindowDelegate();
}
