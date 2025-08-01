using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton; // Reference to the Start button
    [SerializeField] private Button exitButton; // Reference to the Exit button
    //[SerializeField] private Scene levelToLoad;

    public void StartGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
        
        // If running in the editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
