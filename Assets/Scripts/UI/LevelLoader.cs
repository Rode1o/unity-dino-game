using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private string levelToLoad = "GameScene"; // Nombre de la escena que quieres cargar

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
} 