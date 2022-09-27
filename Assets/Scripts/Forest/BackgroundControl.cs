using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public List<BackgroundMover> bgs;
    
    public float speed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bgs.ForEach( bg => {
            bg.Move(this.speed * Time.deltaTime);
        });
    }
}
