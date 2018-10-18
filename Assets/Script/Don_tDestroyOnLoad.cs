using UnityEngine;
using System.Collections;

public class Don_tDestroyOnLoad : MonoBehaviour
{
    static bool created = false;

    void Awake()
    {
        if (created)
            Destroy(this);
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
    }
}
