using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ExplainPanel : MonoBehaviour
{

    [SerializeField] private GameObject _obj;

    public bool _isOpen { get; private set; }
    
    private void Awake()
    {
        _isOpen = false;
        _obj.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && _isOpen)
        {
            _isOpen = false;
            _obj.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !_isOpen)
        {
            _isOpen = true;
            _obj.SetActive(true);
        }
    }
}
