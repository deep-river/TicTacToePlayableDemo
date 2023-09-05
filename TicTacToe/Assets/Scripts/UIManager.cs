using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text resultText;
    public GameObject retryScreen;

    public GameController gameController;
    public Text retryText;
    public Button replayEasyBtn;
    public Button replayMediumBtn;

    public Text roundNoText;
    public Text xScoreText;
    public Text drawTimeText;
    public Text oScoreText;

    private int roundNo;
    private int xScore;
    private int oScore;
    private int drawTimes;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowRetryScreen();
        }
    }

    public void ResetResultUI()
    {
        retryScreen.SetActive(false);
        resultText.text = "";
        retryText.gameObject.SetActive(false);
        ResetScores();
    }

    public void UpdateResultUI(RoundResult result)
    {
        roundNo += 1;
        roundNoText.text = roundNo.ToString();

        switch (result)
        {
            case RoundResult.PlayerWin:
                resultText.text = "You Won!";
                xScore += 1;
                xScoreText.text = xScore.ToString();
                break;
            case RoundResult.PlayerLost:
                resultText.text = "You Lost!";
                oScore += 1;
                oScoreText.text = oScore.ToString();
                break;
            case RoundResult.Draw:
                resultText.text = "It's a Draw!";
                drawTimes += 1;
                drawTimeText.text = drawTimes.ToString();
                break;
        }
        ShowResultText();
    }

    public void ResetScores()
    {
        xScoreText.text = "0";
        drawTimeText.text = "0";
        oScoreText.text = "0";
        roundNo = 1;
        xScore = 0;
        oScore = 0;
        drawTimes = 0;
    }

    public void ShowResultText()
    {
        resultText.gameObject.SetActive(true);
    }

    public void HideResultText()
    {
        resultText.text = "";
    }

    public void ShowRetryScreen()
    {
        retryText.text = "Try Again?";
        retryScreen.SetActive(true);
        retryText.gameObject.SetActive(true);
    }

    public void ShowInitScreen()
    {
        retryText.text = "Select Mode";
        retryScreen.SetActive(true);
    }

    public void EasyModeBtnOnClicked()
    {
        gameController.RestartGame(GameMode.Easy);
    }

    public void MediumModeBtnOnClicked()
    {
        gameController.RestartGame(GameMode.Medium);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
