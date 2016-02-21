using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonUtil : MonoBehaviour {

    public UIManager UIM;

	//public void StartGame()
	//{
	//	GameManager.instance.StartGame ();
	//}

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = 0.0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadArena()
    {
        UIM.LoadArena();
    }
}
