using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerAttack : NetworkBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private float _attackRaycastmaxDistance = 1.5f;
    [SerializeField] private float _particleDuration = 3f;
    [SerializeField] private float _attackDamage = 10f;
    private RaycastHit _hitInfo;
    
    private IInputHandler _inputHandler;
    private ISaveLoadManager _saveLoadManager;
    [SerializeField]private PlayerWeaponController _weaponController;
    [Inject]
    public void Construct(IInputHandler inputHandler, ISaveLoadManager saveLoadManager)
    {
        _inputHandler = inputHandler;
        _saveLoadManager = saveLoadManager;
        
    }

    private void Start()
    {
        if (!isLocalPlayer)
            return;
        //if (_weaponController._isToolInHandle)
        //{
            _inputHandler.AttackPerformed += OnAttackPerformed;
        //}
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
                damageable.ApplyDamage(_weaponController._currentTool.CalculateDamage(damageable.GetType()));
                RpcInstantiateParticle();
                //CmdApplyDamage(_hitInfo.transform.gameObject, _attackDamage);

                if(_hitInfo.transform.TryGetComponent(out Rock rock)) // for now, before we introduce things to save 
                {
                    _saveLoadManager.SaveGame(rock.HP);
                }
            }
        }
    }

    //[Command]
    // private void CmdApplyDamage(GameObject target, float damage)
    // {
    //     //target.GetComponent<IDamageable>().ApplyDamage(_weaponController._currentTool.CalculateDamage(damageable));
    //     RpcInstantiateParticle();
    // }
    //
    [ClientRpc]
    private void RpcInstantiateParticle()
    {
         GameObject particle = Instantiate(_particlePrefab, _hitInfo.point, Quaternion.identity);
         particle.GetComponent<ParticleSystem>().Play();
         Destroy(particle, _particleDuration);
    }
}
