using System;
using Blade.Combat;
using Blade.Core.StatSystem;
using UnityEngine;

public class GetBall : MonoBehaviour
{
    private GameObject player;
    public float moveSpeed = 10f;
    public float followDistance = 30f;

    
    private EntityStat targetCompo;
    [SerializeField] private StatSO targetStat;
    [SerializeField] private float modifyValue;


    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= followDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        targetCompo = other.GetComponentInChildren<EntityStat>();
        
        targetCompo.AddModifier(targetStat, this, modifyValue);
        
        gameObject.SetActive(false);
    }
}
