using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private static string message = "";
    private static bool showMessage = false;

    public static void ShowMessage(string msg)
    {
        message = msg;
        showMessage = true;
    }

    void OnGUI()
    {
        if (showMessage)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 50;
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;

            GUI.Label(new Rect(0, Screen.height / 2 - 50, Screen.width, 100), message, style);

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 50), "Rejouer"))
            {
                showMessage = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 120, 200, 50), "Menu Principal"))
            {
                showMessage = false;
                SceneManager.LoadScene("MainMenu");
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 190, 200, 50), "Quitter"))
            {
                Application.Quit();
                Debug.Log("Quit !");
            }
        }
    }
}