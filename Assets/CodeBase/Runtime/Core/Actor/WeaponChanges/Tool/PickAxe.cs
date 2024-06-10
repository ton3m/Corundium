using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : ITool
{
    private ITool _baseTool;
    
    public float BaseDamage { get; }
    
    // other things
    
    public PickAxe(ITool baseTool, int baseDamageUpgrade = 0)
    {
        _baseTool = baseTool;
        BaseDamage = _baseTool.BaseDamage + baseDamageUpgrade;
    }
    
    public float CalculateDamage(Type hitObjectType)
    {
        float resultDamage = BaseDamage;
        
        if (hitObjectType == typeof(Rock)) // условно
        {
             resultDamage *= 2;  
        }
        else
        {
            resultDamage = 0;
        }
        // else if (hitObjectType == typeof(Tree)) 
        // {
        //     resultDamage /= 2;  
        // }
        
        return resultDamage;
    }
}
