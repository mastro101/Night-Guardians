using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance Map_Menu_Hover_Sound;
    FMOD.Studio.EventInstance Map_Menu_Click_Sound;
    FMOD.Studio.EventInstance Map_Incontro_Hover_Sound;
    FMOD.Studio.EventInstance Map_Incontro_Click_Sound;
    FMOD.Studio.EventInstance Map_Deck_Click_Sound;

    void Awake()
    {
        Map_Menu_Hover_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Map/Map_Menu_Hover");
        Map_Menu_Click_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Map/Map_Menu_Click");
        Map_Incontro_Hover_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Map/Map_Incontro_Hover");
        Map_Incontro_Click_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Map/Map_Incontro_Click");
        Map_Deck_Click_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Map/Map_Deck_Click");
    }

    public void Map_Menu_Hover()
    {
        Map_Menu_Hover_Sound.start();
    }

    public void Map_Menu_Click()
    {
        Map_Menu_Click_Sound.start();
    }

    public void Map_Incontro_Hover()
    {
		//if(LevelManager.levelManagerPointer.LevelMap == actLevelMap)
			Map_Incontro_Hover_Sound.start();
    }

    public void Map_Incontro_Click()
    {
		//if (LevelManager.levelManagerPointer.LevelMap == actLevelMap)
			Map_Incontro_Click_Sound.start();
    }

    public void Map_Deck_Click()
    {
        Map_Deck_Click_Sound.start();
    }
}

