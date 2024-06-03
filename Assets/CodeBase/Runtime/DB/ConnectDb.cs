using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using PUSHKA.MySQL;

public class ConnectDb
{
    public static SqlDataBase DataBase;
    public void Main()
    {
        DataBase= new SqlDataBase("185.252.146.78", "DiplomDb", "sipo", "pas3368845S!");
    }
}
