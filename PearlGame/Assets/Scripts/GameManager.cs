using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI titleText;

    public Button restartButton;
    public Button exploreButton;

    [SerializeField] private Player player;

    private static GameManager instance;

    void Awake()
    {
        // Singleton to ensure only one instance of GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
            return; // Early exit if duplicate found
        }
    }

    void Start()
    {
        titleText.gameObject.SetActive(true);
        exploreButton.gameObject.SetActive(true);
        player.canMove = false;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void LevelComplete()
    {
        levelCompleteText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Reloads game when Restart button clicked
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Shows game title and Explore button to start the game
    public void StartGame()
    {
        titleText.gameObject.SetActive(false);
        exploreButton.gameObject.SetActive(false);
        player.canMove = true;
    }

    // Disable player movement
    public void DisablePlayerMovement()
    {
        player.canMove = false;
    }

    // Enable player movement
    public void EnablePlayerMovement()
    {
        player.canMove = true;
    }
}
