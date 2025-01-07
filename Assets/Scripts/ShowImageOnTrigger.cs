using UnityEngine;
using UnityEngine.UI;

public class ShowImageOnTrigger : MonoBehaviour
{
    public GameObject uiImage; // L'image UI � afficher
    public KeyCode interactionKey = KeyCode.E; // La touche d'interaction
    private bool isPlayerInRange = false;

    void Update()
    {
        // V�rifie si le joueur est proche et appuie sur la touche
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            // Activer ou d�sactiver l'image UI
            uiImage.SetActive(!uiImage.activeSelf);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que le joueur a le tag "Player"
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
