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
        int resultDamage = BaseDamage;
        
        if(hitObjectType == Type.Tree) // условно
        {
            resultDamage *= 2;  
        }
        // other ifs
        
        return resultDamage;
    }
}
