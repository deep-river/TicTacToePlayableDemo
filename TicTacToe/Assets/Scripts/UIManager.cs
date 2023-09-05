using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text resultText;
    public Button replayBtn;

    public GameController gameController;

    private void Start()
    {
        ResetResultUI();
    }

    public void ResetResultUI()
    {
        resultText.text = "";
        replayBtn.gameObject.SetActive(false);
    }

    public void SetResultUI(RoundResult result)
    {
        ShowButtons();
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

    public void ShowButtons()
    {
        replayBtn.gameObject.SetActive(true);
    }

    public void ReplayButtonOnClicked()
    {
        gameController.Restart();
    }
}
