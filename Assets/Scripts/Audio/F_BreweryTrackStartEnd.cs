using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_BreweryTrackStartEnd : MonoBehaviour
{
    void Start()
    {
        F_MusicManager.instance.PlayBreweryMusic();
    }

    private void OnDestroy()
    {
        F_MusicManager.instance.StopBreweryMusic();
    }
}
