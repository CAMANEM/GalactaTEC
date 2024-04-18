using UnityEngine;
using UnityEngine.UI;
public class Healthplayer : MonoBehaviour
{
    public float health = 250;
    public Image barraDeVida;
    public GameObject[] healthStates;

    void Start()
    {
        UpdateHealthBar();
        UpdateHealthState();
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, 250);
        UpdateHealthBar();
        UpdateHealthState();
    }

    void UpdateHealthBar()
    {
        barraDeVida.fillAmount = health / 250;
    }

    void UpdateHealthState()
    {
        foreach (GameObject state in healthStates)
        {
            state.SetActive(false);
        }

        if (health > 166)
            healthStates[2].SetActive(true);
        else if (health > 83)
            healthStates[1].SetActive(true);
        else
            healthStates[0].SetActive(true);
    }
}