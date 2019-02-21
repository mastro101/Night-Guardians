using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance MM_Menu_Hover_Sound;
    FMOD.Studio.EventInstance MM_Menu_Click_Sound;
    FMOD.Studio.EventInstance MM_Incontro_Click_Sound;

    void Awake()
    {
        MM_Menu_Hover_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Main_Menu/MM_Menu_Hover");
        MM_Menu_Click_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Main_Menu/MM_Menu_Click");
        MM_Incontro_Click_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Main_Menu/MM_Incontro_Click");
    }

    public void MM_Menu_Hover()
    {
        MM_Menu_Hover_Sound.start();
    }

    public void MM_Menu_Click()
    {
        MM_Menu_Click_Sound.start();
    }

    public void MM_Incontro_Click()
    {
        MM_Incontro_Click_Sound.start();
    }
}

