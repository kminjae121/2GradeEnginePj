using System;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Button _btn;

    private void Awake()
    {
        _btn.onClick.AddListener(Click);
    }

    public void Click()
    {
        
    }
}
