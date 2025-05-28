using UnityEngine;

public class SpawnEnemyTest : MonoBehaviour
{
    [SerializeField] private GameObject _gameobj;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_gameobj,transform.position,Quaternion.identity);
        }
    }
}
