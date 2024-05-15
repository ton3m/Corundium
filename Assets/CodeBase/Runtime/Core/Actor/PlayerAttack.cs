using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class PlayerAttack : NetworkBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private float _attackRaycastmaxDistance = 1.5f;
    [SerializeField] private float _particleDuration = 3f;
    [SerializeField] private float _attackDamage = 10f;
    private RaycastHit _hitInfo;
    
    private void Start()
    {
        if (!isLocalPlayer)
            return;
        
        _inputHandler.AttackPerformed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        _inputHandler.AttackPerformed -= OnAttackPerformed;
    }
    
    private void OnAttackPerformed()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hitInfo, _attackRaycastmaxDistance))
        {
            if (_hitInfo.transform.TryGetComponent(out IDamageable damageable))
            {
                CmdApplyDamage(_hitInfo.transform.gameObject, _attackDamage);
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
         GameObject particle = Instantiate(_particlePrefab, _hitInfo.point, Quaternion.identity);
         //NetworkServer.Spawn(particle.gameObject); вернуть завтра
         particle.GetComponent<ParticleSystem>().Play();
         Destroy(particle, _particleDuration);
    }
}
