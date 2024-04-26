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
        if (lives < maxLives)   // Verificar si es posible añadir una vida
        {
            lives++;            // Incrementar el contador de vidas
            UpdateLivesDisplay(); // Actualizar la UI de vidas
            Debug.Log("Vida añadida: " + lives);  // Imprimir mensaje en la consola
        }
        else
        {
            Debug.Log("No se puede añadir más vidas. Máximo alcanzado.");  // Imprimir si se alcanza el máximo
        }
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
}
