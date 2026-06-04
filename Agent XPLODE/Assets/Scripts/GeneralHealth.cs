using System;
using UnityEngine;

public class GeneralHealth : MonoBehaviour
{
    public int health = 100;
    public bool isAlive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmg)
    {
        if (dmg < 0)
        {
            throw new ArgumentException("Damage cannot be negative");
        }

        health -= dmg;
        Debug.Log(gameObject + " took " + dmg + " damage");
        if (health <= 0)
        {
            isAlive = false;
        }
    }
}
