using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Références")]
    [SerializeField]
    private MouseLook mouseLook;
    [SerializeField]
    public Animator animator;

    private bool isAttacking = false;

    [Header("Attack")]

    [SerializeField]
    private float attackRange;

    [SerializeField] 
    private float attackDamage;

    [SerializeField]
    LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Afficher le rayon de l'attaque dans la game view


        Debug.DrawRay(mouseLook.transform.position, mouseLook.transform.forward * attackRange, Color.red);
        

        // get l'animation en cours 
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (Input.GetMouseButtonDown(0) && !isAttacking && !stateInfo.IsTag("PlayerAttack"))
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            Attack();
        }
    }

    void Attack()
    {
        RaycastHit hit; // Variable pour stocker les informations de l'objet touché par le RayCast
        if (Physics.Raycast(mouseLook.transform.position, mouseLook.transform.TransformDirection(Vector3.forward), out hit, attackRange, layerMask)) // Le RayCast est en fonction de la direction ou regarde le joueur
        {
            Debug.Log("Hit : " + hit.transform.name); 
            if(hit.transform.CompareTag("Monster"))
            {
                Debug.Log("Monster Hit");
                EnemyAI enemyAI = hit.transform.GetComponent<EnemyAI>();
                enemyAI.TakeDamage(attackDamage);
            }
        }
    }   

    public void AttackFinish() // Fonction appelée à la fin de l'animation d'attaque à l'aide d'un animation event
    {
        isAttacking = false;
        animator.ResetTrigger("Attack");
    }
}
