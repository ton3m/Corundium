using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Zenject;

public class Respawn : MonoBehaviour
{
    private IInputHandler _inputHandler;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _ui;
    [SerializeField] private PlayerHelths _playerHelths;
    [SerializeField] private ActorMotor _actorMotor;
    
    
    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void Start()
    {
        _spawnPoint = GameObject.Find("Player Spawn Point");
        _inputHandler.RespawnPerformed += Respawned;
    }

    private void OnDisable()
    {
        _inputHandler.RespawnPerformed -= Respawned;
    }

    private void Respawned()
    {
        gameObject.transform.position = _spawnPoint.transform.position;
        Debug.Log("Хуй" + _spawnPoint.transform.position);
        _playerHelths._hpPlayer = _playerHelths._maxHpPlayer;
        StartCoroutine(Wait());
        _ui.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        _actorMotor.enabled = true;
        
    }
}
