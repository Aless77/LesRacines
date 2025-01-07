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

        // D�sactiver les templates de dialogue au d�part
        Template_Dialogue.SetActive(false);
        Template_Dialogue_Player.SetActive(false);
        canva.SetActive(false); // Assurez-vous que le canvas est d�sactiv� au d�part

    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position); // distance entre le joueur et l'ennemi
        if (distance < 4 && Input.GetKeyDown(KeyCode.F) && !PlayerMovement.dialogue) // si le joueur est dans le rayon de detection
        {
            canva.SetActive(true);
            PlayerMovement.dialogue = true;

            // Affiche le dialogue de l'NPC d'abord
            NewDialogue("Oh, �tranger ! Qu�est-ce qui t�am�ne ici ?");
            NewDialoguePlayer("Je ne sais pas... Je me suis r�veill� ici, dans la for�t.");
            NewDialogue("Comment �a, tu ne sais pas ? D�o� viens-tu ?");
            NewDialoguePlayer("Eh bien... Je ne sais pas... Je ne me souviens de rien.");
            NewDialogue("Hmmm... Int�ressant. Je pensais que ce n��tait qu�une l�gende, mais... tu es peut-�tre un Enfant Perdu de Vecta.");
            NewDialoguePlayer("Un Enfant Perdu de quoi ? Qu�est-ce que c�est ?");
            NewDialogue("Les Enfants Perdus de Vecta... On dit qu�ils viennent d�un autre monde. Le Dieu Vecta les envoie ici pour une raison myst�rieuse, connue de lui seul.");
            NewDialogue("Tu es le premier que je rencontre... Mais dis-moi, te souviens-tu de quelque chose ? Ton nom, peut-�tre ?");
            NewDialoguePlayer("Mon nom... Je m�appelle... Non... Je ne m�en souviens pas non plus.");
            NewDialogue("�trange... Cela confirme peut-�tre que tu es vraiment un Enfant Perdu de Vecta.");
            NewDialogue("�coute, je ne sais pas si cela pourra t�aider, mais une vieille l�gende parle d�un endroit dans cette for�t... La Colline des Souvenirs.");
            NewDialoguePlayer("La Colline des Souvenirs ?");
            NewDialogue("Oui, il para�t qu�au sommet se trouve une pierre magique. On raconte qu�elle r�v�le la v�rit� sur le pass� de ceux qui la touchent.");
            NewDialoguePlayer("Une pierre magique... Elle pourrait m�aider � retrouver la m�moire ?");
            NewDialogue("C�est ce que dit la l�gende. Mais attention, cet endroit est dangereux. Personne n�y est all� depuis des ann�es. On murmure qu�un monstre terrible y vit. Ceux qui ont tent� de gravir la colline ne sont jamais revenus.");
            NewDialoguePlayer("Je comprends le danger, mais je ne peux pas tourner le dos � mon pass�. C�est mon histoire, mes racines, ce qui me d�finit. Je dois y aller.");
            NewDialogue("Alors, fais attention, �tranger. La for�t est remplie de dangers. Sois prudent et... que Vecta veille sur toi. Bonne chance.");
            canva.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    IEnumerator DisplayPlayerDialogue()
    {
        // Attendre un moment avant de commencer � afficher les dialogues du joueur
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