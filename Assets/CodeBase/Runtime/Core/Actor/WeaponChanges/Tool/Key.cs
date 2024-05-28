using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Key : ITool
{
    private ITool _baseTool;
    
    public int BaseDamage { get; }
    
    // other things
    
    public Key(ITool baseTool, int baseDamageUpgrade = 0)
    {
        _baseTool = baseTool;
        BaseDamage = _baseTool.BaseDamage + baseDamageUpgrade;
    }
    
    public int CalculateDamage(Type hitObjectType)
    {
        int resultDamage = BaseDamage;
        
        // if(typeof(hitObjectType) == typeof(Enemy)) // условно
        // {
        //     resultDamage *= 2;  
        // }
        // other ifs
        
        return resultDamage;
    }
}
