using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_WalkingTrackStartEnd : MonoBehaviour
{

    void Start()
    {
        F_MusicManager.instance.PlayWalkingMusic();
    }

    private void OnDestroy()
    {
        F_MusicManager.instance.StopWalkingMusic();
    }
}
