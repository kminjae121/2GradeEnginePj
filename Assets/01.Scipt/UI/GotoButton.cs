using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoButton : MonoSingleton<GotoButton>
{
    public void GotoLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GotoGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
