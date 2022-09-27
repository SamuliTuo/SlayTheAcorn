using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScaler : MonoBehaviour
{
    private UnityEngine.UI.CanvasScaler scaler;
    // Start is called before the first frame update
    void Start()
    {
        this.scaler = this.GetComponent<UnityEngine.UI.CanvasScaler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Screen.width > Screen.height)
        {
            this.scaler.matchWidthOrHeight = 0f;
            this.scaler.referenceResolution = new Vector2(1280f, 1280f);
        }
        else{
            this.scaler.matchWidthOrHeight = 1f;
            this.scaler.referenceResolution = new Vector2(1000f,1000f);
        }
    }
}
