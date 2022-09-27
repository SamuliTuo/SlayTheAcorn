using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCamera : MonoBehaviour
{
    public BezierCurve curve;
    
    public float phase = 0;
    
    public Vector3 shake;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time * 2f;
        this.transform.position = this.curve.GetPoint(this.phase)+ new Vector3( Mathf.Sin(t)*shake.x,Mathf.Sin(t*0.45f)*shake.y,Mathf.Sin(t*0.65f)*shake.z );
        this.transform.LookAt(this.curve.GetPoint(this.phase)+new Vector3(0f,0f,10f) + this.curve.GetDirection(this.phase)*4f);
    }
}
