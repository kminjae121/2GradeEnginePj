using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BaseStatLibrary : MonoBehaviour
{
    public static BaseStatLibrary instance;
    public Dictionary<string, TextMeshProUGUI> baseTxt;

    [SerializeField] private GameObject _obj;

    public bool _isOpen { get; private set; }
    
    private void Awake()
    {
        instance = this;
        baseTxt = new Dictionary<string, TextMeshProUGUI>();
        _obj.GetComponentsInChildren<TextMeshProUGUI>().ToList().ForEach(UI => baseTxt.Add(UI.name, UI));
        
        _isOpen = false;
        _obj.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _isOpen)
        {
            _isOpen = false;
            _obj.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F) && !_isOpen)
        {
            _isOpen = true;
            _obj.SetActive(true);
        }
    }
}
