using System.Collections;
using UnityEngine;

public class Plaque : MonoBehaviour, IInteractable
{
    [field: SerializeField] public Transform PointForTip { get; private set;  }
    
    [SerializeField] private GameObject _bridge;
    [SerializeField] private GameObject _particle;
    private float _particleDuration = 3f;

    public void Interact()
    {
        //check resource in inventory
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

