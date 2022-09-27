using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public float relativeSpeed = 0f;
    private float acc = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Move(float delta)
    {
        acc += delta*this.relativeSpeed;
        this.transform.localPosition = new Vector3( -acc, this.transform.localPosition.y, this.transform.localPosition.z );
    }
}
