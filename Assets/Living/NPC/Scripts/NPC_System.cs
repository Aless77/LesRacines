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
    public GameObject Template_Dialogue_Player;
    public GameObject canva;
    // Update is called once per frame
    void Start()
    {
        // Get les components par rapport au nom de l'objet
        playerTransform = GameObject.Find("Player").transform;
        player = GameObject.Find("Player").GetComponent<Player>();

        // Désactiver les templates de dialogue au départ
        Template_Dialogue.SetActive(false);
        Template_Dialogue_Player.SetActive(false);
        canva.SetActive(false); // Assurez-vous que le canvas est désactivé au départ

    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position); // distance entre le joueur et l'ennemi
        if (distance < 4 && Input.GetKeyDown(KeyCode.F) && !PlayerMovement.dialogue) // si le joueur est dans le rayon de detection
        {
            canva.SetActive(true);
            PlayerMovement.dialogue = true;

            // Affiche le dialogue de l'NPC d'abord
            NewDialogue("Oh, étranger ! Qu’est-ce qui t’amène ici ?");
            NewDialoguePlayer("Je ne sais pas... Je me suis réveillé ici, dans la forêt.");
            NewDialogue("Comment ça, tu ne sais pas ? D’où viens-tu ?");
            NewDialoguePlayer("Eh bien... Je ne sais pas... Je ne me souviens de rien.");
            NewDialogue("Hmmm... Intéressant. Je pensais que ce n’était qu’une légende, mais... tu es peut-être un Enfant Perdu de Vecta.");
            NewDialoguePlayer("Un Enfant Perdu de quoi ? Qu’est-ce que c’est ?");
            NewDialogue("Les Enfants Perdus de Vecta... On dit qu’ils viennent d’un autre monde. Le Dieu Vecta les envoie ici pour une raison mystérieuse, connue de lui seul.");
            NewDialogue("Tu es le premier que je rencontre... Mais dis-moi, te souviens-tu de quelque chose ? Ton nom, peut-être ?");
            NewDialoguePlayer("Mon nom... Je m’appelle... Non... Je ne m’en souviens pas non plus.");
            NewDialogue("Étrange... Cela confirme peut-être que tu es vraiment un Enfant Perdu de Vecta.");
            NewDialogue("Écoute, je ne sais pas si cela pourra t’aider, mais une vieille légende parle d’un endroit dans cette forêt... La Colline des Souvenirs.");
            NewDialoguePlayer("La Colline des Souvenirs ?");
            NewDialogue("Oui, il paraît qu’au sommet se trouve une pierre magique. On raconte qu’elle révèle la vérité sur le passé de ceux qui la touchent.");
            NewDialoguePlayer("Une pierre magique... Elle pourrait m’aider à retrouver la mémoire ?");
            NewDialogue("C’est ce que dit la légende. Mais attention, cet endroit est dangereux. Personne n’y est allé depuis des années. On murmure qu’un monstre terrible y vit. Ceux qui ont tenté de gravir la colline ne sont jamais revenus.");
            NewDialoguePlayer("Je comprends le danger, mais je ne peux pas tourner le dos à mon passé. C’est mon histoire, mes racines, ce qui me définit. Je dois y aller.");
            NewDialogue("Alors, fais attention, étranger. La forêt est remplie de dangers. Sois prudent et... que Vecta veille sur toi. Bonne chance.");
            canva.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    IEnumerator DisplayPlayerDialogue()
    {
        // Attendre un moment avant de commencer à afficher les dialogues du joueur
        yield return new WaitForSeconds(1f); // Une petite pause avant le dialogue du joueur
        yield return new WaitForSeconds(1f); // Une petite pause avant le prochain message du joueur
        yield return new WaitForSeconds(1f); // Une petite pause avant le prochain message du joueur
        yield return new WaitForSeconds(1f); // Une petite pause avant le prochain message du joueur
        yield return new WaitForSeconds(1f); // Une petite pause avant le prochain message du joueur
        yield return new WaitForSeconds(1f); // Une petite pause avant le prochain message du joueur
        yield return new WaitForSeconds(1f); // Une petite pause avant le prochain message du joueur
    }


    void NewDialogue(string text)
    {
        GameObject template_clone = Instantiate(Template_Dialogue, Template_Dialogue.transform);
        template_clone.transform.parent = canva.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;

    }

    void NewDialoguePlayer(string text)
    {
        GameObject template_clone = Instantiate(Template_Dialogue_Player, Template_Dialogue_Player.transform);
        template_clone.transform.parent = canva.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;

    }
}