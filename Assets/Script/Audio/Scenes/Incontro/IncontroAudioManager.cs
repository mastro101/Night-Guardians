using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncontroAudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance Fight_Cannon_Explosion_Sound;
    FMOD.Studio.EventInstance Fight_Cannon_Fuse_Sound;
    FMOD.Studio.EventInstance Fight_Menu_Click_Sound;
    FMOD.Studio.EventInstance Fight_Deck_Click_Sound;

    void Awake()
    {
        Fight_Cannon_Explosion_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Incontro/Cannone/Fight_Cannon_Explosion");
        Fight_Cannon_Fuse_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Incontro/Cannone/Fight_Cannon_Fuse");
        Fight_Menu_Click_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Map/Map_Menu_Click");
        Fight_Deck_Click_Sound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Map/Map_Deck_Click");
    }

    public void Fight_Cannon_Explosion()
    {
        Fight_Cannon_Explosion_Sound.start();
        Fight_Cannon_Fuse_Sound.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void Fight_Cannon_Fuse_start()
    {
        Fight_Cannon_Fuse_Sound.start();
    }

    public void Fight_Cannon_Fuse_stop()
    {
        Fight_Cannon_Fuse_Sound.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void Fight_Deck_Click()
    {
        Fight_Deck_Click_Sound.start();
    }

    public void Fight_Menu_Click()
    {
        Fight_Menu_Click_Sound.start();
    }

}

