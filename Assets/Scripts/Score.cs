using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static float total;

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        total += Time.deltaTime;
        text.text = Mathf.RoundToInt(total).ToString();
    }

}
