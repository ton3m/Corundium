using UnityEngine;
using Mirror;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine.InputSystem;
public class PlayerHit : NetworkBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private GameObject _particle;
    [SerializeField] private float _maxDistance = 1.5f;
    [SerializeField] private float _durationParticle = 3f;
    private LayerMask _minebleLayer;
    private RaycastHit _hit;
    private void Start()
    {
        _inputHandler.HitInputPressed += OnHit;
    }
    private void OnDisable()
    {
        _inputHandler.HitInputPressed -= OnHit;
    }
    private void OnHit()
    {
        if (!isLocalPlayer)
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hit, _maxDistance))
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()),transform.forward * _maxDistance, Color.cyan,1f);
            if (_hit.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(10);
                InstantiateParticle();
            }
        }
    }
    private void InstantiateParticle()
    {
         GameObject particle = Instantiate(_particle, _hit.point,quaternion.identity);
         NetworkServer.Spawn(particle.gameObject);
         particle.GetComponent<ParticleSystem>().Play();
         Destroy(particle, _durationParticle);
    }
    
}
