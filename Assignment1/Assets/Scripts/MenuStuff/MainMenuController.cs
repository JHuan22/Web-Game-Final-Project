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
        // SaveSystem.LoadPlayer();
    }

    public void SaveButton()
    {
        Debug.Log("Save Button");
        // SaveSystem.SavePlayer();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
