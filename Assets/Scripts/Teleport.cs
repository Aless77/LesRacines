using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Place ce script sur l'objet "TeleportCircle".
/// Quand le joueur (tag "Player") entre, charge la scène cible.
/// </summary>
public class Teleport : MonoBehaviour
{
    [Tooltip("Nom EXACT de la scène à charger (figurant dans Build Settings).")]
    public string targetScene = "3.RoomMemory";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optionnel : effet de fade ici
            SceneManager.LoadScene(targetScene);
        }
    }
}