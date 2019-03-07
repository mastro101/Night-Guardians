using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CloseWindow : MonoBehaviour
{
    Transform CardsInWindow;
	EndCondiction endCondiction;
	CombatManager combatManager;

    public event CloseWindowEvent.CloseWindowDelegate OnClose;

	public void Awake()
	{
		endCondiction = FindObjectOfType<EndCondiction>();
		combatManager = FindObjectOfType<CombatManager>();
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

    public void OpenChooseCardPanel()
    {
        FindObjectOfType<CombatManager>().chooseCardsPanel.gameObject.SetActive(true);
        CardsInWindow = transform.parent.GetChild(0);
        for (int i = 0; i < CardsInWindow.childCount; i++)
        {
            Destroy(CardsInWindow.GetChild(i).gameObject);
        }
        transform.parent.gameObject.SetActive(false);
    }

	public void EndSelection() {
		endCondiction.endSelect = true;
		combatManager.DestroyOldEnemy();
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
