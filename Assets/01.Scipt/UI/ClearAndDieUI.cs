using System;
using UnityEngine;
using UnityEngine.UI;

public class ClearAndDieUI : MonoBehaviour
{
    [SerializeField] private Button _gotoLobbyBtn;
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _quitBtn;

    private void Start()
    {
        _gotoLobbyBtn.onClick.AddListener(GotoButton.Instance.GotoLobby);
        _continueBtn.onClick.AddListener(GotoButton.Instance.ReloadGame);
        _quitBtn.onClick.AddListener(GotoButton.Instance.QuitGame);
    }
}
