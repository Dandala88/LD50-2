using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image title;

    private void Awake()
    {
        title.enabled = true;
    }

    private void OnEnable()
    {
        GameManager.OnStartGame += OnGameStart;
        GameManager.OnEndGame += OnGameEnd;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= OnGameStart;
        GameManager.OnEndGame -= OnGameEnd;
    }

    private void OnGameStart()
    {
        title.enabled=false;
    }

    private void OnGameEnd()
    {
        title.enabled = true;
    }
}
