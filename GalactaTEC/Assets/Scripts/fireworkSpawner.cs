using System.Collections;
using UnityEngine;

public class fireworkSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] fireworkPrefabs;
    [SerializeField] RectTransform canvasRectTransform;
    private int maxNumberOfFireworks = 3;
    private Vector2 randomPosition;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(spawnFirework), 0.5f, 3.0f);
    }

    private void spawnFirework()
    {
        int max = Random.Range(1, maxNumberOfFireworks);
        StartCoroutine(spawnFireworkCoroutine(max));
    }

    private IEnumerator spawnFireworkCoroutine(int max)
    {
        for (int i = 1; i <= max; i++)
        {
            currentIndex = Random.Range(0, fireworkPrefabs.Length);
            randomPosition = getRandomPositionInCanvas();
            Instantiate(fireworkPrefabs[currentIndex], randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private Vector2 getRandomPositionInCanvas()
    {
        float randomX1 = Random.Range(-8f, -6f);
        float randomX2 = Random.Range(6f, 8f);
        float randomFX = Random.Range(0, 2);
        if (randomFX == 0)
        {
            randomFX = randomX1;
        }
        else
        {
            randomFX = randomX2;
        }
        float randomY = Random.Range(-4f, 4f);

        return new Vector2(randomFX, randomY);
    }
}
