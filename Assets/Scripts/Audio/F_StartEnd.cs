using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_StartEnd : MonoBehaviour
{

    void Start()
    {
        F_MusicManager.instance.PlayCombatMusic();
    }

    private void OnDestroy()
    {
        F_MusicManager.instance.StopCombatMusic();
    }
}
