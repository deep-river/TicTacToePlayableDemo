using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text resultText;

    private void Start()
    {
        ResetText();
    }

    public void ResetText()
    {
        resultText.text = "";
    }

    public void SetResultText(RoundResult result)
    {
        switch(result)
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
}
