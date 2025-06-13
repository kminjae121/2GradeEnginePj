using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoSingleton<SettingManager>
{

    [SerializeField] private GameObject[] _settingUiList = new GameObject[3];

    public RectTransform mainUI;

    protected override void Awake()
    {
        base.Awake();
        ForceShowCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && LoadingGame.Instance.IsEnd)
        {
            if (IsSettingUIActive())
                CloseSetting();
            else
                OpenSoundSetting();
        }
    }
    
    public void OpenSoundSetting() => OpenSetting("Sound");

    private void OpenSetting(string name)
    {
        ClearSetting();
        GameObject target = FindSetting(_settingUiList, name);

        if (target != null)
        {
            target.SetActive(true);
            StartCoroutine(WaitToPauseGame());
        }
    }

    public void CloseSetting()
    {
        ClearSetting();
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void ClearSetting()
    {
        foreach (var ui in _settingUiList)
        {
            if (ui != null)
                ui.SetActive(false);
        }
    }

    private GameObject FindSetting(GameObject[] settingList, string name)
    {
        return settingList.FirstOrDefault(setting => setting != null && setting.name.Contains(name));
    }

    private bool IsSettingUIActive()
    {
        return _settingUiList.Any(ui => ui != null && ui.activeSelf);
    }

    private IEnumerator WaitToPauseGame()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        ForceShowCursor(); 
        Time.timeScale = 0f;
    }

    private void ForceShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
