using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{


    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
}
