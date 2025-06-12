using UnityEngine;

public class Setting : MonoBehaviour
{
    public void OpenSetting()
    {
        SettingManager.Instance.OpenSoundSetting();
    }

    public void CloseSetting()
    {
        SettingManager.Instance.CloseSetting();
    }
}
