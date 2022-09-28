using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverlayCanvas : MonoBehaviour
{
    public TMP_Text acorns;
    
    private float currentAcorns = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int target = PlayerInventory.instance.acornCount;
        this.currentAcorns += (target-currentAcorns)/40f;
        if(this.currentAcorns > target-0.25f) this.currentAcorns = target;
        acorns.text = Mathf.FloorToInt( this.currentAcorns ).ToString();
    }
}
