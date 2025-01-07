using UnityEngine;
using UnityEngine.UI;

public class ShowImageOnTrigger : MonoBehaviour
{
    public GameObject uiImage; // L'image UI à afficher
    public KeyCode interactionKey = KeyCode.E; // La touche d'interaction
    private bool isPlayerInRange = false;

    void Update()
    {
        Debug.Log("Player merde");
        // Vérifie si le joueur est proche et appuie sur la touche
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            Debug.Log("tremplis conditions");
            // Activer ou désactiver l'image UI
            uiImage.SetActive(!uiImage.activeSelf);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que le joueur a le tag "Player"
        {
            Debug.Log("Player in range");
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player not in range");
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
