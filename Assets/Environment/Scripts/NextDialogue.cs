using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    int index = 2; // Indice initial de l'enfant a activer
   
    // Update is called once per frame
    void Update()
    {
        // Verifie si un clic droit de la souris est detecte et s'il y a plus d'un enfant attache a ce GameObject
        if (Input.GetMouseButtonDown(1) && transform.childCount > 1) 
        {
            // Verifie si le dialogue est en cours dans PlayerMovement
            if (PlayerMovement.dialogue)
            {
                // Recupere le transform de l'enfant a l'indice index
                Transform child = transform.GetChild(index);
                
                // Verifie si l'enfant n'est pas nul (c'est-a-dire s'il existe)
                if (child != null)
                {
                    // Active le GameObject de cet enfant
                    child.gameObject.SetActive(true);
                    
                    // Incremente l'indice
                    index += 1;
                    
                    // Si l'indice est egal au nombre total d'enfants dans la hierarchie, reinitialise l'indice a 2 et met le booleen dialogue a false
                    if (transform.childCount == index)
                    {
                        index = 2;
                        PlayerMovement.dialogue = false;
                    }
                }
            }
            else
            {
                // Si le dialogue n'est pas en cours, desactive le GameObject auquel ce script est attache
                gameObject.SetActive(false);
            }
        }
    }
}
