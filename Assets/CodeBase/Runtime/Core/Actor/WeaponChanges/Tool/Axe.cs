using System;
using UnityEngine;

public class Axe : ITool
{
    public float BaseDamage { get; }
    
    // other things
    
    public Axe(ITool baseTool, float baseDamageUpgrade = 0)
    {
        BaseDamage = baseTool.BaseDamage + baseDamageUpgrade;
    }
    
    public float CalculateDamage(Type hitObjectType)
    {
        Debug.Log(hitObjectType);
        float resultDamage = BaseDamage;
        if(hitObjectType == typeof(Tree)) // условно
        {
             resultDamage *= 2; 
             Debug.Log("Its Tree");
        }
        else if (hitObjectType == typeof(Rock))
        {
            resultDamage /= 2;
        }
        return resultDamage;
    }
}
