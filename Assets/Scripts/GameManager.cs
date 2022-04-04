using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameStart;
    public delegate void StartGame();
    public static event StartGame OnStartGame;
    public FadeScreen fadescreen;

    public delegate void EndGame();
    public static event EndGame OnEndGame;
    public float maxSilenceTime;

    private float silenceTime;
    private static bool endGame;

    public static void GameStart()
    {
        gameStart = true;
        endGame = false;
        OnStartGame.Invoke();
    }

    private void Update()
    {
        if (RippleManager.ripples.Count == 0 && gameStart && !endGame)
        {
            silenceTime += Time.deltaTime;
            fadescreen.ChangeAlpha(silenceTime / maxSilenceTime);
            if (silenceTime > maxSilenceTime)
            {
                endGame = true;
                gameStart = false;
                foreach (Interactive interactive in RippleManager.interactives)
                {
                    Destroy(interactive.gameObject);
                }
                RippleManager.interactives.Clear();
                silenceTime = 0;
                fadescreen.ChangeAlpha(0);
                OnEndGame.Invoke();
            }
        }
        else
        {
            if(silenceTime > 0)
                silenceTime -= Time.deltaTime;
            fadescreen.ChangeAlpha(silenceTime / maxSilenceTime);
        }

    }
}
