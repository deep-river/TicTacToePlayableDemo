
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public Cell[] cells = new Cell[9];

    public void ResetBoard()
    {
        foreach (var cell in cells)
        {
            cell.isSet = false;
            cell.ResetIcon();
            cell.GetComponent<Button>().interactable = true;
        }
    }

    public void DisableButtons()
    {
        foreach (var cell in cells)
        {
            cell.GetComponent<Button>().interactable = false;
        }
    }
}
