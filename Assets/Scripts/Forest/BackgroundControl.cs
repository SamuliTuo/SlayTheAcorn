using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public List<BackgroundMover> bgs;
    
    public WalkIn player;
    
    public float speed = 0f;
    private float walked = 0f;
    
    private int current= 0;
    
    public List<float> stopAt;
    public float exitAt;
    
    public bool stopped = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.walked += this.speed * Time.deltaTime*2f;
        bgs.ForEach( bg => {
            bg.Move(this.speed * Time.deltaTime*2f);
        });
        
        
        if( this.current < this.stopAt.Count && this.walked >= stopAt[current] )
        {
            this.stopped = true;
            this.current++;
            player.state = WalkIn.WalkState.stop;
            if(this.current >= this.stopAt.Count ){
                this.player.current = 0;
                this.player.runCombat = true;
              Invoke("movetonext",1f);  
            } 
        }
        if(!this.stopped && player.state == WalkIn.WalkState.stop)
        {
            player.state = WalkIn.WalkState.startFromStop;
        }
        if(this.walked >= this.exitAt){
            player.state = WalkIn.WalkState.walkOut;
        }
        
    }
    
    void movetonext()
    {
         PlayerInventory.instance.moveToBattle();

    }
}
