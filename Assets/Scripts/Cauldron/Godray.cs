using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Godray : MonoBehaviour
{
    public Transform quad1;
    public Transform quad2;
    public AnimationCurve appear;
    
    public float targetScale = 2f;
    
    public float appearTime;
    public bool visible = false;
    private float appearPhase = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.visible)
            this.appearPhase += Time.deltaTime/this.appearTime;
        else
            this.appearPhase -= Time.deltaTime/this.appearTime;
            
        this.appearPhase = Mathf.Clamp( this.appearPhase, 0f, 1f);
        float s = appear.Evaluate(appearPhase) * targetScale;
        this.quad1.localScale = new Vector3(s,s,s);
        this.quad2.localScale = new Vector3(s,s,s);
        
        this.quad1.rotation = Quaternion.Euler( 0f, 0f, Time.time );
        this.quad2.rotation = Quaternion.Euler( 0f, 0f, -Time.time*0.95f );
        
    }
}
