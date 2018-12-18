using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Don_tDestroyOnLoad : MonoBehaviour
{
    static bool created = false;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            Destroy(this.gameObject);

        if (created)
            Destroy(this);
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
    }
}
