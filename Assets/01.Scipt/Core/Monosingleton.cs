using UnityEngine;

using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    
    public static T Instance
    {
        get
        {
            if (_instance is null)
            {
                T manager = FindFirstObjectByType<T>();
                if (manager != null)
                    _instance = manager.GetComponent<T>();
            }

            if (_instance is null)
            {
                string objectName = typeof(T).Name;
                GameObject manager = new GameObject(objectName);
                _instance = manager.AddComponent<T>();
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        T[] managers = FindObjectsByType<T>(FindObjectsSortMode.None);

        if (managers.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}
