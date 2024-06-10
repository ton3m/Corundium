using System;
using Mirror.Examples.Chat;
using UnityEngine;
using MySql.Data.MySqlClient;
using UnityEngine.InputSystem.XR.Haptics;

public class ConnectDb : MonoBehaviour
{
    public static MySqlConnection connection;
    private int _userId;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
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

    private void OnApplicationQuit()
    {
        if (connection != null)
        {
            connection.Close();
            Debug.Log("Database connection closed.");
        }
    }

    private float GetFloatValue(string query, MySqlParameter[] parameters, string columnName)
    {
        float value = 0f;
        MySqlCommand cmd = new MySqlCommand(query, connection);

        if (parameters != null)
        {
            cmd.Parameters.AddRange(parameters);
        }

        try
        {
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                value = dataReader.GetFloat(columnName);
            }
            dataReader.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return value;
    }

    public float GetHPPlayer()
    {
        string query = "SELECT hp_player FROM Player WHERE id_user = @user_id";
        MySqlParameter[] parameters = { new MySqlParameter("@user_id", _userId) };
        return GetFloatValue(query, parameters, "hp_player");
    }

    public float GetHPEnemy(int idEnemy)
    {
        string query = "SELECT max_hp_enemy FROM Enemys WHERE id_enemy = @id_enemy";
        MySqlParameter[] parameters = { new MySqlParameter("@id_enemy", idEnemy) };
        return GetFloatValue(query, parameters, "max_hp_enemy");
    }

    public float GetHPRes(string nameRes)
    {
        string query = "SELECT max_hp_resource FROM Resources WHERE name_resource = @name_resource";
        MySqlParameter[] parameters = { new MySqlParameter("@name_resource", nameRes) };
        return GetFloatValue(query, parameters, "max_hp_resource");
    }

    public float GetDamage(string moduleName)
    {
        string query = "SELECT damage_module FROM ToolModule WHERE name_module = @moduleName";
        MySqlParameter[] parameters = { new MySqlParameter("@moduleName", moduleName) };
        return GetFloatValue(query, parameters, "damage_module");
    }

    public bool SignIn(string login, string password)
    {
        string query = "SELECT id_user FROM Users WHERE login_user = @login AND password_user = @password";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@login", login);
        cmd.Parameters.AddWithValue("@password", password);

        try
        {
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                _userId = dataReader.GetInt32("id_user");
                dataReader.Close();
                Debug.Log(_userId);
                return true;
            }
            dataReader.Close();
            return false;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return false;
        }
    }

    public bool SignUp(string login, string password)
    {
        string query = "INSERT INTO Users (login_user, password_user) VALUES (@login, @password)";
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

    private void SelectId(string login)
    {
        string query = "SELECT id_user FROM Users WHERE login_user = @login";
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