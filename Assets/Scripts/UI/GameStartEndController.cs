using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartEndController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
