using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class TpHome : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;

    private void Start()
    {
        _spawnPoint = GameObject.Find("Player Spawn Point");
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -1)
        {
            gameObject.transform.position = _spawnPoint.transform.position;
        }
    }
}
