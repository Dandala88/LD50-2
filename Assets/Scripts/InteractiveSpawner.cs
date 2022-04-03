using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSpawner : MonoBehaviour
{
    public float frequency;
    public float frequencyRange;
    public PlayerController player;
    public float minSpeed;
    public float maxSpeed;
    public float directionAngleMin;
    public float directionAngleMax;
    public float lifespan;

    public List<Interactive> interactives = new List<Interactive>();

    private void OnValidate()
    {
        if(frequencyRange >= frequency)
            frequencyRange = frequency - 0.1f;
        if (frequencyRange < 0)
            frequencyRange = 0;
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }
    private IEnumerator SpawnCoroutine()
    {
        float finalSeconds = Random.Range(frequency - frequencyRange, frequency + frequencyRange);
        yield return new WaitForSeconds(finalSeconds);
        float posX = Random.Range(-player.playfieldWidth, player.playfieldWidth);
        float posY = Random.Range(-player.playfieldHeight, player.playfieldHeight);

        float speed = Random.Range(minSpeed, maxSpeed);
        float angle = Random.Range(directionAngleMin, directionAngleMax);
        Vector2 direction = (player.transform.position - new Vector3(posX, posY) * angle).normalized;
        int ri = Random.Range(0, interactives.Count);
        Interactive clone = Instantiate(interactives[ri]);
        clone.transform.position = new Vector3(posX, posY);
        clone.Move(direction, speed, lifespan);

        StartCoroutine(SpawnCoroutine());
    }
}
