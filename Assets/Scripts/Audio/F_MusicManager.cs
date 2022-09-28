using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_MusicManager : MonoBehaviour
{
    public string Bank;

    public string BankStrings;

    public static F_MusicManager instance;

    private FMOD.Studio.EventInstance CombatMusic;

    private FMOD.Studio.EventInstance WalkingMusic;

    private FMOD.Studio.EventInstance BreweryMusic;

    private void Awake()
    {

        instance = this;
        DontDestroyOnLoad(gameObject);

        FMODUnity.RuntimeManager.LoadBank(Bank);
        FMODUnity.RuntimeManager.LoadBank(BankStrings);

        CombatMusic = FMODUnity.RuntimeManager.CreateInstance("event:/MusicAmb/Combat Track");

        //  WalkingMusic = FMODUnity.RuntimeManager.CreateInstance("");

        //  BreweryMusic = FMODUnity.RuntimeManager.CreateInstance("");
    }




    public void PlayCombatMusic()
    {
        CombatMusic.start();
    }
    public void StopCombatMusic()
    {
        CombatMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        CombatMusic.release();
    }

    public void PlayWalkingMusic()
    {
        WalkingMusic.start();
    }

    public void StopWalkingMusic()
    {
        WalkingMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        WalkingMusic.release();
    }

    public void PlayBreweryMusic()
    {
        BreweryMusic.start();
    }

    public void StopBreweryMusic()
    {
        BreweryMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        BreweryMusic.release();
    }

}
