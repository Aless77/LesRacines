using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enabled = false; // On d�sactive le script de d�placement
        PlayerAttack playerAttack = GetComponent<PlayerAttack>();
        playerAttack.enabled = false; // On d�sactive le script d'attaque
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Die"); // On d�clenche l'animation de mort
        yield return new WaitForSeconds(5); // On attend 5 secondes
        Destroy(gameObject); // On d�truit le joueur
    }

    public bool IsDead()
    {
        return isDead;
    }
}
