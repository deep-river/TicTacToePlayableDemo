using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Sprite[] icons = new Sprite[2];
    public Image iconImage;

    [HideInInspector] public bool isSet = false;

    private GridValue val = 0;
    public GridValue Val
    {
        get { return val; }
        set
        {
            if (!isSet)
            {
                val = value;
                SetIconImage();
            }
        }
    }

    public void SetValue(GridValue val)
    {
        Val = val;
    }

    private void SetIconImage()
    {
        if (iconImage != null)
        {
            iconImage.sprite = icons[(int)val - 1];
            iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 255f);
        }
    }

    public void ResetIcon()
    {
        iconImage.sprite = null;
        iconImage.color = new Color(iconImage.color.r, iconImage.color.g, iconImage.color.b, 0f);
    }
}
