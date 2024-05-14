using System;
using UnityEngine;
using Mirror;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerHit : NetworkBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private GameObject _particle;
    [SerializeField] private float _maxDistanceRaycast = 1.5f;
    [SerializeField] private float _durationParticle = 3f;
    [SerializeField] private float _damagePicexe = 10f;
    private LayerMask _minebleLayer;
    private RaycastHit _hit;
    private void Start()
    {
        if (!isLocalPlayer)
            return;
        
        _inputHandler.HitInputPressed += OnHit;
    }
    private void OnDisable()
    {
        _inputHandler.HitInputPressed -= OnHit;
    }
    
    private void OnHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hit, _maxDistanceRaycast))
        {
            //Debug.DrawRay(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()),transform.forward * _maxDistance, Color.cyan,1f);
            if (_hit.transform.TryGetComponent(out IDamageable damageable))
            {
                CmdApplyDamage(_hit.transform.gameObject, _damagePicexe);
            }
        }
    }
    [Command]
    private void CmdApplyDamage(GameObject target, float damage)
    {
        target.GetComponent<IDamageable>().ApplyDamage(damage);
        RpcInstantiateParticle();
    }
    [ClientRpc]
    private void RpcInstantiateParticle()
    {
         GameObject particle = Instantiate(_particle, _hit.point,quaternion.identity);
         //NetworkServer.Spawn(particle.gameObject); вернуть завтра
         particle.GetComponent<ParticleSystem>().Play();
         Destroy(particle, _durationParticle);
    }
    
}
