using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliveryStatus : MonoBehaviour
{
    [SerializeField] private GameObject deliverySuccess;
    [SerializeField] private GameObject deliveryFailure;
    [SerializeField] private GameObject hudUI;

    public void ShowSuccessScreen()
    {
        hudUI.SetActive(false);
        deliverySuccess.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowFailureScreen()
    {
        hudUI.SetActive(false);
        deliveryFailure.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
