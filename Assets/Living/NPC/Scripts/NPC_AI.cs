using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcAI : IA
{
    private Transform playerTransform;

    void Start()
    {
        // Get les components par rapport au nom de l'objet
        playerTransform = GameObject.Find("Player").transform;
        base.Start();
    }

    void Update()
    {
        if (IsDead)
            return;

        if (!HasDestination && agent.remainingDistance < 0.75f && base.targetIsInArea(playerTransform)) // si le NPC n'a pas de destination
        {
            GetNewDestination(); // trouver une nouvelle destination
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    
    }
}
