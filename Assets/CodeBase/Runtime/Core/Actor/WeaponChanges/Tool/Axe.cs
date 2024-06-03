using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using MySql.Data.MySqlClient;

public class Axe : ITool
{
    public float BaseDamage { get; }
    private ConnectDb _db;
    
    // other things
    
    public Axe(ITool baseTool, float baseDamageUpgrade = 0)
    {
        _db = new ConnectDb();
        BaseDamage = _db.GetDamage("Axe");
        Debug.Log(BaseDamage);
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
