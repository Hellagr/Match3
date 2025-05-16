using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneRestarter : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
