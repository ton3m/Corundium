using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Key : ITool
{
    private ITool _baseTool;
    
    public float BaseDamage { get; }
    
    // other things
    
    public Key(ITool baseTool, int baseDamageUpgrade = 0)
    {
        _baseTool = baseTool;
        BaseDamage = _baseTool.BaseDamage + baseDamageUpgrade;
    }
    
    public float CalculateDamage(Type hitObjectType)
    {
        float resultDamage = BaseDamage;
        
        //if(hitObjectType == typeof(Enemy))// условно
        // {
        //     resultDamage *= 2;  
        // }
        // else
        // {
        //     resultDamage /= 2;
        // }
        
        return resultDamage;
    }
}
