using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI distanceText;
    public Transform player;
    public Transform criminal;
    public static GameUI instance;

    private float timer = 0f;
    private bool gameRunning = true;

    void Awake()
    {
        instance = this;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!gameRunning) return;

        // Timer
        timer += Time.deltaTime;
        int minutes = (int)(timer / 60f);
        int seconds = (int)(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Distance
        if (player != null && criminal != null)
        {
            float distance = Vector3.Distance(player.position, criminal.position);
            distanceText.text = distance.ToString("F1") + "m";

            if (distance < 5f)
                distanceText.color = Color.green;
            else if (distance < 15f)
                distanceText.color = Color.yellow;
            else
                distanceText.color = Color.red;
        }
    }

    public static void ShowMessage(string msg)
    {
        if (instance == null) return;
        instance.gameRunning = false;
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
    }
}