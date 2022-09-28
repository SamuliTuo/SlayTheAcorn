using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public BrewResult result;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void startBrew()
    {

        this.result.animateIn = true;

        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/UI/PotionCreate", gameObject);

    }
    
    public void brewingDone()
    {
        PlayerInventory.instance.moveToForest();
    }
}
