using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    public void Quit ()
    {
        Debug.Log("APPLICATION QUIT!");
        Application.Quit();
    }

    public void Retry ()
    {
        GameMaster._remainingLives = 3;
        GameMaster.Money = 100;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
