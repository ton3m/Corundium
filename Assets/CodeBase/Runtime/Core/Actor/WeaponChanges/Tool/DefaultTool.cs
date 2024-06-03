using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefaultTool : ITool
{
    public float BaseDamage { get; }
    
    public DefaultTool(float baseDamage)
    {
        BaseDamage = baseDamage;
    }
    
    public float CalculateDamage(Type hitObjectType)
    {
        return BaseDamage;
    }
}
