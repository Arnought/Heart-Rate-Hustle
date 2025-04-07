using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Lv 1");
    }

    public void Level2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lv 2");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
