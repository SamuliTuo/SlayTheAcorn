using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkIn : MonoBehaviour
{
    public enum WalkState {
        walkIn,
        start,
        walking,
        stop,
        startFromStop,
        walkOut
    };
    
    public BackgroundControl bgC;
    public AnimationCurve inCurve;
    public AnimationCurve startCurve;
    public AnimationCurve startBGCurve;
    public AnimationCurve stopCurve;
    public AnimationCurve outCurve;
    
    public float timeToIn;
    public float timeToOut;
    public float startTime;
    public float stopTime;
    
    private float inPhase = 0f;
    private float startPhase = 0f;
    private float startFromStopPhase = 0f;
    private float stopPhase = 0f;
    private float outPhase = 0f;
    
    
    /** 0=in, 1=start, 2=walking, 3=stop, 4=start from stop, 5=out */
    public WalkState state = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // In
        if(this.state == WalkState.walkIn){
            this.inPhase += Time.deltaTime/this.timeToIn;
            this.inPhase = Mathf.Clamp(this.inPhase, 0f,1f);
            this.transform.localPosition = new Vector3(
                this.inCurve.Evaluate(this.inPhase),
                this.transform.localPosition.y,
                this.transform.localPosition.z
            );
            if(this.inPhase >=1f && (Input.GetMouseButtonDown(0) || (Input.touchCount>0 && Input.GetTouch(0).phase ==TouchPhase.Began)))
            {
                this.state = WalkState.start;
            }
        }
        else{
            this.inPhase = 0f;
        }
        // Start walking from in
        if(this.state == WalkState.start){
            this.startPhase += Time.deltaTime/this.startTime;
            this.startPhase = Mathf.Clamp(this.startPhase, 0f,1f);
            this.transform.localPosition = new Vector3(
                this.startCurve.Evaluate(this.startPhase),
                this.transform.localPosition.y,
                this.transform.localPosition.z
            );
            this.bgC.speed = this.startBGCurve.Evaluate(this.startPhase);
        }
        else{
            this.startPhase = 0f;
        }
        // Stop walking
        if(this.state == WalkState.stop){
            this.stopPhase += Time.deltaTime/this.stopTime;
            this.stopPhase = Mathf.Clamp(this.stopPhase, 0f,1f);
            this.transform.localPosition = new Vector3(
                this.stopCurve.Evaluate(this.stopPhase),
                this.transform.localPosition.y,
                this.transform.localPosition.z
            );
            this.bgC.speed = this.startBGCurve.Evaluate(1f-this.stopPhase);
        }
        else{
            this.stopPhase = 0f;
        }
        //Start walking from stop
        if(this.state == WalkState.startFromStop){
            this.startFromStopPhase += Time.deltaTime/this.timeToOut;
            this.startFromStopPhase = Mathf.Clamp(this.startFromStopPhase, 0f,1f);
            this.transform.localPosition = new Vector3(
                this.stopCurve.Evaluate(1f-this.startFromStopPhase),
                this.transform.localPosition.y,
                this.transform.localPosition.z
            );
            this.bgC.speed = this.startBGCurve.Evaluate(this.startFromStopPhase);
        }
        else{
            this.startFromStopPhase = 0f;
        }
        if(this.state == WalkState.walkOut){
            this.outPhase += Time.deltaTime/this.timeToOut;
            this.outPhase = Mathf.Clamp(this.outPhase, 0f,1f);
            this.transform.localPosition = new Vector3(
                this.outCurve.Evaluate(this.outPhase),
                this.transform.localPosition.y,
                this.transform.localPosition.z
            );
            //this.bgC.speed = this.startBGCurve.Evaluate(1f-this.stopPhase);
        }
        else{
            this.outPhase = 0f;
        }
    }
}
