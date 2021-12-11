using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    [SerializeField]
    public void QuitGame()
    {
        Application.Quit(); // this only closes the game when the game is a stand alone build
    }
}
