using UnityEngine;

public class BtnManager : MonoBehaviour
{
    public void GotoLobby() => GotoButton.Instance.GotoLobby();

    public void GotoGame(string name)
    {
        GotoButton.Instance.GotoGame(name);
    }
    public void QuitGame() => GotoButton.Instance.QuitGame();   
}
