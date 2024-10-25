using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health =100;
    public float healthPercent;
    public PauseMenu pause;
    void Start()
    {
       
    }
    public void Damage(int amount)
    {
        health -= amount;
        if (health < 0) health = 0;
    }
    void Update()
    {
        healthPercent = (float)health / 100f;
        if (health <= 0)
        {
            
            pause.Gameover();
        }
    }
}
