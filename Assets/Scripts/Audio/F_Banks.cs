using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Banks : MonoBehaviour
{
    public string Bank;
    public string BankStrings;

    private void Awake()
    {
        FMODUnity.RuntimeManager.LoadBank(Bank);
        FMODUnity.RuntimeManager.LoadBank(BankStrings);
    }
}
