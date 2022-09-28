using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewResult : MonoBehaviour
{
    public Material firePotion;
    public Material ghostPotion;
    public Material heartbreakPotion;
    public Material paralysisPotion;
    public Material hallusinationPotion;
    public Material shieldingPotion;
    
    public Godray godray;
    
    public bool animateIn = false;
    
    public float animationPhase =0f;
    public AnimationCurve scaleCurve;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.animateIn) animationPhase += Time.deltaTime;
        
        float s = scaleCurve.Evaluate(animationPhase);
        this.transform.localScale = new Vector3(s,s,s);
        
        this.godray.visible = animationPhase >0f;
    }
}
