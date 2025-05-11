using System.Reflection;
using _01.Scipt.Player.Player;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BarrerCompo : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsBlockObj;

    [SerializeField] private Player _player;

    public bool isPalling => _player._barrerSkill.isPalling;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.transform.gameObject.layer) & _whatIsBlockObj) != 0 && isPalling)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

           // other.TryGetComponent(out Bullet bullet);
            //bullet._isReflect = true;

            
            if (rb != null)
            {
                Vector3 currentVelocity = rb.linearVelocity;
                
                Vector3 direction = - currentVelocity.normalized;
                
                float forceMagnitude = currentVelocity.magnitude;
                
                rb.AddForce(direction * forceMagnitude * 2f, ForceMode.VelocityChange);
            }
        }
        else if ((1 << other.transform.gameObject.layer & _whatIsBlockObj) != 0)
        {
            other.gameObject.SetActive(false);
        }
    }
}
