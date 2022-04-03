using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image title;
    public Image credits;

    private void Awake()
    {
        title.enabled = true;
        credits.enabled = false;
    }

    private void OnEnable()
    {
        GameManager.OnStartGame += OnGameStart;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= OnGameStart;
    }

    private void OnGameStart()
    {
        title.enabled=false;
    }
}
