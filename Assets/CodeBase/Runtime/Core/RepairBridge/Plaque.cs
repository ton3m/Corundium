using System.Collections;
using CodeBase.Runtime.Core.Inventory;
using TMPro;
using UnityEngine;
using Zenject;

public class Plaque : MonoBehaviour, IInteractable
{
    [field: SerializeField] public Transform PointForTip { get; private set;  }
    
    [SerializeField] private GameObject _bridge;
    [SerializeField] private GameObject _particle;
    [SerializeField] private TMP_Text _textCount;
    
    [Header("Repair Item")]
    [SerializeField] private Item _itemToRepair;
    [SerializeField] private int _ItemCountToRepair;
    private float _particleDuration = 3f;
    private IInventory _inventory;

    [Inject]
    public void Construct(IInventory inventory)
    {
        _inventory = inventory;
    }

    private void Start()
    {
        _textCount.text = _ItemCountToRepair.ToString();
    }
    
    public void Interact()
    {
        //check resource in inventory
        var item = _inventory.GetItemSlot(_itemToRepair);

        if (item == null)
            return;

        if (_inventory.TryRemove(_itemToRepair, _ItemCountToRepair) == false)
        {
            Debug.Log("Лох, у тебя не хватает ресурсов на постройку, тебе не хватает: " + (item.Quantity - _ItemCountToRepair) + " кол-ва ");
            return;
        }
        
        PlayParticle();
        StartCoroutine(TimeToBuild());
    }

    private void PlayParticle()
    {
        GameObject particle = Instantiate(_particle, _bridge.transform.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
        Destroy(particle, _particleDuration);
    }

    IEnumerator TimeToBuild()
    {
        yield return new WaitForSeconds(_particleDuration);
        _bridge.SetActive(true);
        Destroy(gameObject);
    }
    
}

