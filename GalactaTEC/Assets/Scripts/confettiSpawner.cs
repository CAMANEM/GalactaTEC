using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confettiSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] confettiPrefabs;
    [SerializeField] Transform[] spawnPoints;

    private int currentIndex = 0;
    private GameObject currentConfetti;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnConfettiSequence());
    }

    private IEnumerator SpawnConfettiSequence()
    {
        while (true)
        {
            if (currentConfetti != null)
            {
                Destroy(currentConfetti);
            }

            currentConfetti = Instantiate(confettiPrefabs[currentIndex], spawnPoints[currentIndex].position, spawnPoints[currentIndex].rotation);

            Animator animator = currentConfetti.GetComponent<Animator>();
            float animationLength = 0f;

            if (animator != null)
            {
                animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            }
            else
            {
                Debug.LogWarning("No Animator component found on the confetti prefab. Using default duration.");
                animationLength = 1f; // Default duration if no Animator is found
            }

            yield return new WaitForSeconds(animationLength);

            currentIndex = (currentIndex + 1) % confettiPrefabs.Length;
        }
    }
}
