using TMPro;
using UnityEngine;

public class GameOverSCreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverText; // Reference to the TextMeshProUGUI component for displaying game over text

    private void Start()
    {
        gameObject.SetActive(false); // Initially hide the game over screen
    }

    public void RestartGame()
    {
        // Logic to restart the game, e.g., reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        // Logic to load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your actual main menu scene name
    }

    public void QuitGame()
    {
        // Logic to quit the game, e.g., exit the application
        Application.Quit();
        
        // If running in the editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ShowGameOverScreen(string message)
    {
        // Set the game over text and make the UI visible
        gameOverText.text = message;
        gameObject.SetActive(true); // Ensure the GameObject is active to show the UI
    }

    public void HideGameOverScreen()
    {
        // Hide the game over screen
        gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameOverText.SetText("Game Over!"); // Set the game over text
        gameObject.SetActive(true); // Show the game over screen
    }

    public void Victory()
    {
        gameOverText.SetText("You Win!"); // Set the victory text
        gameObject.SetActive(true); // Show the game over screen
    }
}
