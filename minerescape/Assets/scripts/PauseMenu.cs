using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1; // penting! agar tidak freeze di menu
        SceneManager.LoadScene("MainMenu"); // ganti sesuai nama scene menu kamu
    }

    public void ExitGame()
    {
        Debug.Log("Keluar dari game");
        Application.Quit();
    }
}
