using UnityEngine;
using System.Collections;

public class CloseWindow : MonoBehaviour
{
    Transform CardsInWindow;

    public event CloseWindowEvent.CloseWindowDelegate OnClose;

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
