using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class RecieceRecord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeUI;
    [SerializeField] private TextMeshProUGUI _StatUI;


    private void Update()
    {
        _timeUI.text = PlayerRecordSendManager.Instance.maxTime;
        _StatUI.text = PlayerRecordSendManager.Instance.maxScore;
    }
}
