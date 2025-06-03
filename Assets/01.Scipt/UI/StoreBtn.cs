using UnityEngine;

public class StoreBtn : MonoBehaviour
{
    [SerializeField] private GameObject _storeUI;

    public void EnterStore()
    {
        _storeUI.SetActive(true);
    }

    public void ExitStore()
    {
        _storeUI.SetActive(false);
    }
}
