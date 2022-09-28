using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_AnimationSFX : MonoBehaviour
{
    public string Event1;

    public void PlayEvent1()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event1, gameObject);
    }


}
