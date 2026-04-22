using UnityEngine;

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

            // Bouton rejouer
            if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 50, 150, 40), "Rejouer"))
            {
                showMessage = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                );
            }
        }
    }
}