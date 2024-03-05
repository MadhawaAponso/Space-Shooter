using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;

    public void LoadGame() //load game
    {
        SceneManager.LoadScene("Game"); // can use 1 also
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // main menu loading
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay)); // game over
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");//just letting know we are quitting
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay) //delay after the explosion
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

}
