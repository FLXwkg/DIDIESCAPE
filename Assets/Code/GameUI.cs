using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI messageText;
    public static GameUI instance;

    void Awake()
    {
        instance = this;
        gameOverPanel.SetActive(false);
    }

    public static void ShowMessage(string msg)
    {
        if (instance == null) return;
        instance.messageText.text = msg;
        instance.gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Rejouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quitter()
    {
        Application.Quit();
        Debug.Log("Quit !");
    }
}