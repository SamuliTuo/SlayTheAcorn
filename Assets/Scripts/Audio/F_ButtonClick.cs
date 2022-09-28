using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_ButtonClick : MonoBehaviour
{

    public void onClick()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/UI/Click", gameObject);
    }
}
