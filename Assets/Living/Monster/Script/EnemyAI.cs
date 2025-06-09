using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : IA
{

    [Header("Référence")]

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Player player;

    [Header("Stats")]

    [SerializeField]
    private float chaseSpeed;

    [SerializeField]
    private float detecttionRadius;

    [SerializeField]
    private float attackDamage;

    [SerializeField]
    private float attackRadius;

    [SerializeField]
    private float attackDelay;

    private bool isAttacking;


    // Start is called before the first frame update
    void Start()
    {
        // Get les components par rapport au nom de l'objet
        playerTransform = GameObject.Find("Player").transform;
        player = GameObject.Find("Player").GetComponent<Player>();
        base.linkedObject = GameObject.Find("CrystalsGold");
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)  {return;} // si l'ennemi est mort, on ne fait rien

        float distance = Vector3.Distance(playerTransform.position, transform.position); // distance entre le joueur et l'ennemi
        if (distance < detecttionRadius) // si le joueur est dans le rayon de detection
        {
            ChasePlayer(distance); // l'ennemi poursuit le joueur
        }
        else if (base.targetIsInArea(playerTransform))
        {
            PassOrNewDestination(); // On ne fait rien ou trouve une nouvelle destination
        }

        animator.SetFloat("Speed", agent.velocity.magnitude); // mettre a jour l'animation de deplacement de l'ennemi
    }

    private void ChasePlayer(float distance)
    {
        agent.speed = chaseSpeed; // l'ennemi se met a courir
        if (!SoundsManager.IsPlayingFightSound()) // si l'ennemi ne poursuit pas le joueur mais que le joueur vient de rentrer dans le rayon de detection
        {
            ChangeSounds(); // changer le son de de fond
        }
        Quaternion rotation = Quaternion.LookRotation(playerTransform.position - transform.position); // rotation de l'ennemi vers le joueur
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5); // rotation fluide

        if (!isAttacking) // si l'ennemi n'attaque pas et que le joueur n'est pas mort
        {
            if (distance < attackRadius && !player.IsDead()) // si le joueur est dans le rayon d'attaque
            {
                StartCoroutine(Attack()); // l'ennemi attaque
            }
            else
            {
                agent.SetDestination(playerTransform.position); // l'ennemi se dirige vers le joueur
                animator.ResetTrigger("Attack");
            }
        }
    }

    public void PassOrNewDestination() // On ne fait rien ou trouve une nouvelle destination
    {
        if (SoundsManager.IsPlayingFightSound())
        {
            ResetAudio(); // remettre le son de fond
        }
        if (!HasDestination && agent.remainingDistance < 0.75f) // si l'ennemi n'a pas de destination
        {
            GetNewDestination(); // trouver une nouvelle destination
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true; // l'ennemi attaque
        agent.isStopped = true; // l'ennemi s'arrete
        animator.SetTrigger("Attack"); // lancer l'animation d'attaque
        player.TakeDamage(attackDamage); // infliger des degats au joueur
        yield return new WaitForSeconds(attackDelay); // attendre un temps avant de pouvoir attaquer a nouveau
        agent.isStopped = false; // l'ennemi reprend sa route
        isAttacking = false; // l'ennemi n'attaque plus
    }



    private void OnDrawGizmos() // dessine un cercle rouge autour de l'ennemi pour visualiser le rayon de detection, non visible dans le jeu
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detecttionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
