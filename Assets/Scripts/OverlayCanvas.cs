using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverlayCanvas : MonoBehaviour
{
    public TMP_Text acorns;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        acorns.text = PlayerInventory.instance.acornCount.ToString();
    }
}
