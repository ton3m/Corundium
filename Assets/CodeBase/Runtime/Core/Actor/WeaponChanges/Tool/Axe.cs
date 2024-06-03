using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : ITool
{
    public float BaseDamage { get; }
    
    // other things
    
    public Axe(ITool baseTool, float baseDamageUpgrade = 0)
    {
        //BaseDamage = baseTool.BaseDamage + baseDamageUpgrade;
        BaseDamage = 0;

        ConnectDb.DataBase.SelectQuery("select damage_module from ToolModule where name_module = 'Axe'", out DataTable dataTable);
    }
    
    public float CalculateDamage(Type hitObjectType)
    {
        Debug.Log(hitObjectType);
        float resultDamage = BaseDamage;
        if(hitObjectType == typeof(Tree)) // условно
        {
             resultDamage *= 2; 
             Debug.Log("Its rock");
        }
        else if (hitObjectType == typeof(Rock))
        {
            resultDamage /= 2;
        }
        return resultDamage;
    }
}
