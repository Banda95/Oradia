using UnityEngine;
using UnityEngine.SceneManagement;


public class UIHandler : MonoBehaviour {

    public GameObject pausePanel;

    private bool isPaused = false;

    void Awake()
    {
        pausePanel.SetActive(false);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0.0f;
    }

    public void UnPause()
    {
        pausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        UnPause();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Pause(); }
    }
   
}
