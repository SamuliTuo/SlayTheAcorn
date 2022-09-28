using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPotionController : MonoBehaviour
{
    public void SetActiveState(bool state)
    {
        gameObject.SetActive(state);
    }
}
