using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // DISABLE others
        int bt = PlayerInventory.instance.battle;
        for(int i = 0 ; i < 4; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive( bt == i );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
