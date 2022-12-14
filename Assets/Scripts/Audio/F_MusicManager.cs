using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_MusicManager : MonoBehaviour
{

    bool audioResumed = false;

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

        WalkingMusic = FMODUnity.RuntimeManager.CreateInstance("event:/MusicAmb/Walking Track");

        BreweryMusic = FMODUnity.RuntimeManager.CreateInstance("event:/MusicAmb/Brewing Track");
    }

    public void Start()
    {
        if (!audioResumed)
        {
            var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
            //  Debug.Log(result);
            result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
            //  Debug.Log(result);
            audioResumed = true;
        }
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
