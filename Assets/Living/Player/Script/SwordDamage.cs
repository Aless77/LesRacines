using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SwordDamage : MonoBehaviour
{
    [Tooltip("Temps minimal entre deux coups sur la m�me cible (en secondes)")]
    public float cooldownPerTarget = 1.5f;

    public PlayerAttack _playerAttack;
    private Dictionary<EnemyAI, float> _lastHitTime = new Dictionary<EnemyAI, float>();

    void Start()
    {
        // R�cup�re l�instance PlayerAttack via le tag
        var playerGO = GameObject.FindWithTag("Player");
        if (playerGO == null)
            Debug.LogError("SwordDamage: aucun GameObject tagg� 'Player' trouv�.");
        else
            _playerAttack = playerGO.GetComponent<PlayerAttack>();

        // V�rifie qu�on est bien sur un trigger
        var col = GetComponent<Collider>();
        if (!col.isTrigger)
            Debug.LogWarning("SwordDamage: ce collider devrait �tre en trigger pour la d�tection de d�g�ts.");
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("SwordDamage: OnTriggerStay " + other.name);
        // Ne r�agit qu�aux monstres
        if (!other.CompareTag("Monster")) return;
        if (!(other.GetComponent<EnemyAI>() is EnemyAI enemy)) return;

        Debug.Log("SwordDamage: " + enemy.name + " touch� !");
        // V�rifie le cooldown
        _lastHitTime.TryGetValue(enemy, out float lastTime);
        if (Time.time - lastTime < cooldownPerTarget) return;

        // R�cup�re les d�g�ts depuis PlayerAttack
        float dmg = _playerAttack != null
                    ? _playerAttack.attackDamage
                    : 0f;

        // Inflige les d�g�ts et m�morise le timestamp
        enemy.TakeDamage(dmg);
        _lastHitTime[enemy] = Time.time;
    } 
}
