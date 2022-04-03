using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameStart;
    public delegate void StartGame();
    public static event StartGame OnStartGame;

    public static void GameStart()
    {
        gameStart = true;
        OnStartGame.Invoke();
    }
}
