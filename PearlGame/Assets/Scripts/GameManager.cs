using System.Collections;
using System.Collections.Generic;
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


    // Start is called before the first frame update
    void Start()
    {
        titleText.gameObject.SetActive(true);
        exploreButton.gameObject.SetActive(true);
        player.canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelComplete()
    {
        levelCompleteText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        titleText.gameObject.SetActive(false);
        exploreButton.gameObject.SetActive(false);
        player.canMove = true;
    }

}
