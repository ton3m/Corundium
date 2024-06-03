using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using MySql.Data.MySqlClient;

public class ConnectDb:MonoBehaviour
{
    public static MySqlConnection connection;
    public void Start()
    {
        string connectionString = "Server=185.252.146.78;Database=DiplomDb;User ID=sipo;Password=pas3368845S!;Pooling=true;";
        connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            Debug.Log("Database connected!");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }

    void OnApplicationQuit()
    {
        if (connection != null)
        {
            connection.Close();
            Debug.Log("Database connection closed.");
        }
    }

    public void InsertPerson(string name, int age)
    {
        string query = "INSERT INTO persons (name, age) VALUES (@name, @age)";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@age", age);

        try
        {
            cmd.ExecuteNonQuery();
            Debug.Log("Person inserted.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }

    public float GetDamage(string moduleName)
    {
        float damageModule = 0f;
        string query = "SELECT damage_module FROM ToolModule WHERE name_module = @moduleName";
        MySqlCommand cmd = new MySqlCommand(query, ConnectDb.connection);
        cmd.Parameters.AddWithValue("@moduleName", moduleName);

        try
        {
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                damageModule = Convert.ToSingle(result);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return damageModule;
    }
}