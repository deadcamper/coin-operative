using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    public HintMenu hintMenu;

    public Button startGame;
    public Button hintsAndTips;
    public Button exitToMainMenu;
    public Button exitGame;

    // Start is called before the first frame update
    void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            exitGame.gameObject.SetActive(false);
        }

        if (startGame)
            startGame.onClick.AddListener(StartGame);
        if (hintsAndTips)
            hintsAndTips.onClick.AddListener(ActivateHintMenu);
        if (exitToMainMenu)
            exitToMainMenu.onClick.AddListener(MainGame);
        if (exitGame)
            exitGame.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("CoinOp");
    }

    private void MainGame()
    {
        SceneManager.LoadScene("CoinOpTitleScreen");
    }

    private void ActivateHintMenu()
    {
        gameObject.SetActive(false);
        hintMenu.gameObject.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
