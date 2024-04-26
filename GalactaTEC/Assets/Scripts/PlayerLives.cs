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
        if (lives < maxLives)   // Verificar si es posible a�adir una vida
        {
            lives++;            // Incrementar el contador de vidas
            UpdateLivesDisplay(); // Actualizar la UI de vidas
            Debug.Log("Vida a�adida: " + lives);  // Imprimir mensaje en la consola
        }
        else
        {
            Debug.Log("No se puede a�adir m�s vidas. M�ximo alcanzado.");  // Imprimir si se alcanza el m�ximo
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
