using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSpawner : MonoBehaviour
{
    public float waveFrequency;
    public int waveEntityTotal;
    public float frequency;
    public float frequencyRange;
    public PlayerController player;
    public float minSpeed;
    public float maxSpeed;
    public float directionAngleMin;
    public float directionAngleMax;
    public float lifespan;
    [Tooltip("The lower the number the more even the spread. Negative numbers are allowed. 0 normal weighted distribution.")]
    public int spread;

    public List<Interactive> interactives = new List<Interactive>();

    private int currentEntityTotal;

    private void OnValidate()
    {
        if(frequencyRange >= frequency)
            frequencyRange = frequency - 0.1f;
        if (frequencyRange < 0)
            frequencyRange = 0;
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

    public void OnGameStart()
    {
        StartCoroutine(SpawnCoroutine(5, 0));
    }

    public void OnGameEnd()
    {
        StopAllCoroutines();
    }

    private IEnumerator WaveCoroutine()
    {
        currentEntityTotal = 0;
        yield return new WaitForSeconds(waveFrequency);
        StartCoroutine(SpawnCoroutine(frequency, frequencyRange));
    }

    private IEnumerator SpawnCoroutine(float secs, float range)
    {
        float finalSeconds = Random.Range(secs - range, secs + range);
        yield return new WaitForSeconds(finalSeconds);
        float posX = Random.Range(-player.playfieldWidth, player.playfieldWidth);
        float posY = Random.Range(-player.playfieldHeight, player.playfieldHeight);

        float speed = Random.Range(minSpeed, maxSpeed);
        float angle = Random.Range(directionAngleMin, directionAngleMax);
        Vector2 direction = (player.transform.position - new Vector3(posX, posY) * angle).normalized;
        Interactive clone = Instantiate(WeigthedRandomInteractive(spread));
        clone.transform.position = new Vector3(posX, posY);
        clone.Move(direction, speed, lifespan);
        currentEntityTotal++;
        if(currentEntityTotal < waveEntityTotal)
            StartCoroutine(SpawnCoroutine(frequency, frequencyRange));
        else
            StartCoroutine(WaveCoroutine());
    }

    private Interactive WeigthedRandomInteractive(int spread)
    {
        int total = 0;
        List<int> respectives = new List<int>();
        for(int i = 0; i < interactives.Count; i++)
        {
            total += interactives.Count - (i + spread);
            respectives.Add(total);
        }
        float roll = Random.Range(1, total);
        for (int i = 0; i < respectives.Count; i++)
        {
            if(roll < respectives[i])
                return interactives[i];
        }
        return interactives[0];
    }
}
