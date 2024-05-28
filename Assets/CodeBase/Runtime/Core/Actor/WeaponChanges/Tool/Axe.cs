using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : ITool
{
    public int BaseDamage { get; }
    
    // other things
    
    public Axe(ITool baseTool, int baseDamageUpgrade = 0)
    {
        BaseDamage = baseTool.BaseDamage + baseDamageUpgrade;
    }
    
    public int CalculateDamage(Type hitObjectType)
    {
        Debug.Log(hitObjectType);
        int resultDamage = BaseDamage;
        if(hitObjectType == typeof(Rock)) // условно
        {
             resultDamage *= 2; 
             Debug.Log("Its rock");
        }
        
        
        return resultDamage;
    }
}
