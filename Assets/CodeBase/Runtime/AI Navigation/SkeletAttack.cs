using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletAttack : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private float _daseDamage;
    private IEnumerator _cdAttack;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        StartCoroutine("DeActiveCollide");
        if (collision.gameObject.GetComponent<CharacterController>() != null)
        {
            collision.gameObject.GetComponent<IDamageable>().ApplyDamage(_daseDamage);
            
        }
    }
    IEnumerator DeActiveCollide()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(1.3f);
        _collider.enabled = true;
    }
}
