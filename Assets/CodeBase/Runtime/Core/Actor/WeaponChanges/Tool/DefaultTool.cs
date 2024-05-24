using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTool: ITool
{
    public int BaseDamage { get; }
    
    public DefaultTool(int baseDamage)
    {
        BaseDamage = baseDamage;
    }
    
    public int CalculateDamage(Type hitObjectType)
    {
        return BaseDamage;
    }
}
