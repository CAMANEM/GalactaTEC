using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3;        // Vidas actuales del jugador
    public int maxLives = 5;     // M�ximo de vidas que el jugador puede tener
    public Image[] livesUI;      // Array de im�genes para la UI de vidas

    void Start()
    {
        UpdateLivesDisplay();   // Actualizar la visualizaci�n de vidas al inicio
    }

    // M�todo para a�adir una vida
    public void AddLife()
    {
        AddLives(1);  // A�adir una vida
    }

    // M�todo para a�adir m�ltiples vidas
    public void AddLives(int amount)
    {
        int newLives = lives + amount;
        if (newLives > maxLives)
        {
            lives = maxLives;
            Debug.Log("No se puede a�adir m�s vidas. M�ximo alcanzado.");
        }
        else
        {
            lives = newLives;
            Debug.Log("Vidas a�adidas: " + amount + ", Total vidas: " + lives);
        }
        UpdateLivesDisplay();  // Actualizar la UI de vidas
    }

    // Actualizar la UI basada en la cantidad de vidas actuales
    void UpdateLivesDisplay()
    {
        for (int i = 0; i < livesUI.Length; i++)
        {
            livesUI[i].enabled = i < lives;  // Activar o desactivar la imagen de la vida
        }
        Debug.Log("UI de vidas actualizada.");  // Mensaje al actualizar la UI
    }

    public void subtractLife()
    {
        lives--;
    }
}
