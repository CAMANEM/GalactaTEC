using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3;        // Vidas actuales del jugador
    public int maxLives = 5;     // Máximo de vidas que el jugador puede tener
    public Image[] livesUI;      // Array de imágenes para la UI de vidas

    void Start()
    {
        UpdateLivesDisplay();   // Actualizar la visualización de vidas al inicio
    }

    // Método para añadir una vida
    public void AddLife()
    {
        AddLives(1);  // Añadir una vida
    }

    // Método para añadir múltiples vidas
    public void AddLives(int amount)
    {
        int newLives = lives + amount;
        if (newLives > maxLives)
        {
            lives = maxLives;
            Debug.Log("No se puede añadir más vidas. Máximo alcanzado.");
        }
        else
        {
            lives = newLives;
            Debug.Log("Vidas añadidas: " + amount + ", Total vidas: " + lives);
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
