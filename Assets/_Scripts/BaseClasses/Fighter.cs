using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public bool isAlive = true;
    public int currentHealth;
    public int maxHealth = 100;
    //public int armor;                           // броня

    //Push
    //protected Vector3 pushDirection;
    


    void Awake()
    {
        currentHealth = maxHealth;        
    }



    // All fighters can ReceiveDamage / Die
    protected virtual void ReceiveDamage(int dmg)
    {
        
        currentHealth -= dmg;       

        // Push (убрал пока что (заменил))
        //pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
                       
        //Death
        if (currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }       
    }

    protected virtual void Death()
    {
        Debug.Log(transform.name + " died.");
    }
}