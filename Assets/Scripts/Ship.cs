using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Ship Target;

    //1 is for attack and 2 is for defend
    public int State;
    
    public int side;

    [SerializeField]
    private float health;

    private Turnmanager tm;

    [SerializeField]
    private float attackdamage;


    void Start()
    {
        this.tm = FindObjectOfType<Turnmanager>();
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        print(this.name + " Attacked " + Target + "!");
    }

    string getName()
    {
        return name;
    }

    void takeDamage(float amount)
    {
        if (this.health - amount <= 0)
        {
            this.die();
        }
        else
        {
            this.health -= amount;
        }
    }

    public void setTarget(Ship s)
    {
        this.Target = s;
    }
    public Ship GetTarget()
    {
        return this.Target;
    }

    void die()
    {
        Debug.Log(this + " died");
    }
}
