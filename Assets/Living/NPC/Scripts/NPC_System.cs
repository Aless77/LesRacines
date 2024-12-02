using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPC_System : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Player player;
    
    
    public GameObject Template_Dialogue;
    public GameObject canva;
    // Update is called once per frame
    void Start()
    {
        // Get les components par rapport au nom de l'objet
        playerTransform = GameObject.Find("Player").transform;
        player = GameObject.Find("Player").GetComponent<Player>();
        
    }
    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position); // distance entre le joueur et l'ennemi
        if (distance < 4 && Input.GetKeyDown(KeyCode.F) && !PlayerMovement.dialogue) // si le joueur est dans le rayon de detection
        {
            canva.SetActive(true);
            PlayerMovement.dialogue = true ; 
            NewDialogue("Hi");
            NewDialogue("My name is Lucy");
            NewDialogue("Four Minotaurs live near our village");
            NewDialogue("Find the four rocks and you will find them");
            NewDialogue("Be careful, oh sacred defender");
            canva.transform.GetChild(1).gameObject.SetActive(true);
            
        }
        
    }

    void NewDialogue(string text){
        GameObject template_clone = Instantiate(Template_Dialogue,Template_Dialogue.transform);
        template_clone.transform.parent = canva.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }
    /*void OnTriggerEnter(collider other)
    {
        if(other.name == "player"){
            player_detection = true;
        }
    }
    void OnTriggerExit(collider other)
    {
        player_detection = false;
        
    }*/
}
