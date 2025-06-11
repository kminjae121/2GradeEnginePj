using System;
using Blade.Combat;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody _rbCompo;
    private void Start()
    {
        _rbCompo = GetComponent<Rigidbody>();
        _rbCompo.AddForce(transform.forward * 8, ForceMode.Impulse);
        Destroy(this, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInChildren<EntityHealth>().ApplyDamage(2,transform.position,null, null);
            gameObject.SetActive(false);
            CameraShakingManager.instance.ShakeCam(0.2f,0.2f,3,20);
        }
    }
}
