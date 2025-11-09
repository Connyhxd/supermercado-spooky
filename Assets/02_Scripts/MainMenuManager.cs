using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro");
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu_de_inicio");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
