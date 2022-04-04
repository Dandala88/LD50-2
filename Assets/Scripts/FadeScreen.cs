using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeAlpha(float newAlpha)
    {
        float r = image.color.r;
        float g = image.color.g;
        float b = image.color.b;
        image.color = new Color(r, g, b, newAlpha);
    }

}
