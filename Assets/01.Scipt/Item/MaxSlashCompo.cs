using System;
using Blade.Combat;
using Blade.Enemies.Skeletons;
using UnityEngine;

public class MaxSlashCompo : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 10f;

    private EntitySkillCompo _skillCompo;

    [SerializeField] private LayerMask _whatIsPlayer;

    // 회전을 참조할 외부 오브젝트 (프로퍼티로 설정)
    public Transform TargetRotationSource { get; set; }
    
    

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        
        if (player != null)
        {
            _skillCompo = player.GetComponentInChildren<EntitySkillCompo>();
        }
        
        if (TargetRotationSource != null)
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            float targetY = TargetRotationSource.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(currentRotation.x, targetY, currentRotation.z);
        }
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _whatIsPlayer) != 0)
        {
            EntityHealth health = other.GetComponent<EntityHealth>();
            if (health != null && _skillCompo != null)
            {
                PlayerFuryManager.Instance.RaiseFury(10);
                PlayerComboSystem.Instance.RaiseCombo(3);
                CameraShakingManager.instance.ShakeCam(0.2f, 0.5f, 5, 20);
                health.ApplyDamage(_skillCompo.skillDamage * 2, Vector3.zero, null, null);
                other.GetComponent<EnemySkeletonSlave>().ChangeJumpChannelEvent();
            }
        }
    }
}