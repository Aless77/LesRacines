using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{

    [Header("Références")]
    [SerializeField]

    public NavMeshAgent agent;

    public Animator animator;

    [Header("Stats")]
    [SerializeField]

    public float maxHealth;

    public float walkSpeed;

    private float currentHealth;

    [Header("Wandering movement")]
    [SerializeField]

    public float waitTimeMin;

    public float waitTimeMax;

    public float wanderingDistanceMin;

    public float wanderingDistanceMax;

    [Header("Audio")]
    [SerializeField]

    private SoundsManager soundsManager;

    protected SoundsManager SoundsManager
    {
        get { return soundsManager; }
    }

    private float delayBeforeDestroy = 10f;
    private bool isDead;
    protected bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
    private bool hasDestination;
    public bool HasDestination
    {
        get { return hasDestination; }
        set { hasDestination = value; }
    }

    // Start is called before the first frame update
    public void Start()
    {
        // audioSource est un component de l'objet Main Camera qui est un enfant de l'objet Player faire une recherche en passant par l'objet Player
        soundsManager = GameObject.Find("SoundsManager").GetComponent<SoundsManager>();
        currentHealth = maxHealth;
    }

    public void GetNewDestination()
    {
        agent.speed = walkSpeed; // l'ennemi marche
        StartCoroutine(IGetNewDestination());
    }

    IEnumerator IGetNewDestination()
    {
        HasDestination = true;
        yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax)); // attendre un temps aleatoire

        Vector3 newDestination = transform.position;
        newDestination += Random.Range(wanderingDistanceMin, wanderingDistanceMax) * new Vector3(Random.Range(-1f, 1), 0, Random.Range(-1f, 1)).normalized; // choisir une nouvelle destination aleatoire

        NavMeshHit hit;

        if (NavMesh.SamplePosition(newDestination, out hit, wanderingDistanceMax, NavMesh.AllAreas)) // si la nouvelle destination est sur le sol
        {
            agent.SetDestination(hit.position); // l'ennemi se dirige vers la nouvelle destination
        }

        HasDestination = false;

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) // si l'ennemi n'a plus de vie
        {
            currentHealth = 0;
            IsDead = true;
            StartCoroutine(Death()); // l'ennemi meurt
        }
    }

    IEnumerator Death()
    {
        agent.isStopped = true; // l'ennemi s'arrete
        animator.SetTrigger("Die"); // lancer l'animation de mort
        agent.ResetPath();
        ResetAudio(); // remettre le son de fond
        yield return new WaitForSeconds(delayBeforeDestroy); // attendre un temps avant de detruire l'ennemi
        Destroy(gameObject); // detruire l'ennemi
    }

    public void ChangeSounds()
    {
        soundsManager.PlaySoundsFight(); // lancer le son de combat
    }

    public void ResetAudio()
    {
        soundsManager.ResetAmbienceSound(); // remettre le son de fond
    }
}
