using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public Cell[] cells = new Cell[9];
    public GameController gameController;

    public void BindGridClickEvent()
    {
        for (var i = 0; i < cells.Length; i++)
        {
            int id = i;
            cells[i].GetComponent<Button>().onClick.AddListener(() => gameController.PlayerMove(id));
        }
    }

    public bool PlaceMark(int index, GridValue mark)
    {
        Button btn = cells[index].GetComponent<Button>();
        if (btn != null && btn.interactable)
        {
            btn.interactable = false;
            cells[index].SetValue(mark);
            return true;
        }
        return false;
    }

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
