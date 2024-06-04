using System;
using Mirror.Examples.Chat;
using UnityEngine;
using MySql.Data.MySqlClient;

public class ConnectDb : MonoBehaviour
{
    public static MySqlConnection connection;
    private int _userId;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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

    public float GetDamage(string moduleName)
    {
        float damageModule = 0f;
        string query = "SELECT damage_module FROM ToolModule WHERE name_module = @moduleName";
        MySqlCommand cmd = new MySqlCommand(query, connection);
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

    public bool SignIn(string login, string password)
    {
        string query =  "SELECT id_user FROM Users WHERE login_user = @login AND password_user = @password";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@login", login);
        cmd.Parameters.AddWithValue("@password", password);
        try
        {
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                _userId = dataReader.GetInt32("id_user");
                return true;
            }
            else
            {
                return false;
            }
            dataReader.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }
    }
    public bool SignUp(string login, string password)
    {
        string query =  "INSERT INTO Users (login_user, password_user) VALUES (@login,@password)";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@login", login);
        cmd.Parameters.AddWithValue("@password", password);
        try
        {
            cmd.ExecuteNonQuery();
            SelectId(login);
            
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            
            return false;
        }
    }
    public float GetDamageModule(string moduleName)
    {
        float damageModule = 0f;
        string query = "SELECT damage_module FROM ToolModule WHERE name_module = @moduleName";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@moduleName", moduleName);

        try
        {
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                damageModule = dataReader.GetFloat("damage_module");
            }
            dataReader.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return damageModule;
    }

    private void SelectId(string login)
    {
        string query =  "INSERT INTO Users (login_user, password_user) VALUES (@login,@password)";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@login", login);
        try
        {
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                _userId = dataReader.GetInt32("id_user");
            }
            dataReader.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
}