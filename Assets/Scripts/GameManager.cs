using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public TMP_InputField inputField;
    public ContentController contentController;

    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver(false);
        }
    }

    public void GameOver(bool isWin)
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
        inputField.enabled = false;
        contentController.UpdateWinLoseText(isWin);
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
