using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowOptions()
    {
        Debug.Log("Show Options");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
