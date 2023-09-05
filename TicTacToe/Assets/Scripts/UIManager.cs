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

    private void Start()
    {
        ResetResultUI();
    }

    public void ResetResultUI()
    {
        retryScreen.SetActive(false);
        resultText.text = "";
        retryText.gameObject.SetActive(false);
    }

    public void SetResultUI(RoundResult result)
    {
        ShowRetryScreen();
        switch (result)
        {
            case RoundResult.PlayerWin:
                resultText.text = "You Won!";
                break;
            case RoundResult.PlayerLost:
                resultText.text = "You Lost!";
                break;
            case RoundResult.Draw:
                resultText.text = "It's a Draw!";
                break;
        }
    }

    public void ShowRetryScreen()
    {
        retryScreen.SetActive(true);
        retryText.gameObject.SetActive(true);
    }

    public void ShowInitScreen()
    {
        retryScreen.SetActive(true);
    }

    public void EasyModeBtnOnClicked()
    {
        gameController.Restart(GameMode.Easy);
    }

    public void MediumModeBtnOnClicked()
    {
        gameController.Restart(GameMode.Medium);
    }
}
