using System;
using Blade.Combat;
using UnityEngine;

public class SlashCompo : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 10f;
    private EntitySkillCompo _skillCompo;

    [SerializeField] private LayerMask _whatIsPlayer;

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Transform playerTransform = player.transform;
            _skillCompo = player.GetComponentInChildren<EntitySkillCompo>();
            
            Vector3 currentRotation = transform.rotation.eulerAngles;
            float playerY = playerTransform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(currentRotation.x, playerY, currentRotation.z);
        }
        else
        {
            Debug.LogWarning("Player not found with tag.");
        }
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // 설정된 회전 방향 기준으로 앞으로 이동
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _whatIsPlayer) != 0)
        {
            EntityHealth health = other.GetComponent<EntityHealth>();
            if (health != null && _skillCompo != null)
            {
                health.ApplyDamage(_skillCompo.skillDamage, Vector3.zero, null, null);
            }
        }
    }
}