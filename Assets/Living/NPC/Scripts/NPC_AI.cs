using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NpcAI : IA
{


    void Start()
    {
    }

    void Update()
    {
        if (IsDead)
            return;

        if (!HasDestination && agent.remainingDistance < 0.75f) // si le NPC n'a pas de destination
        {
            GetNewDestination(); // trouver une nouvelle destination
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    
    }
}
