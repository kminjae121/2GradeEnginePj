using UnityEngine;

public class OpenStage : MonoBehaviour
{
    [SerializeField] private GameObject _stageUI;

    public void EnterStore()
    {
        _stageUI.SetActive(true);
    }

    public void ExitStore()
    {
        _stageUI.SetActive(false);
    }
}