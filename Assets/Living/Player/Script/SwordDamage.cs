using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SwordDamage : MonoBehaviour
{
    [Tooltip("Temps minimal entre deux coups sur la même cible (en secondes)")]
    public float cooldownPerTarget = 1.5f;

    private PlayerAttack _playerAttack;
    private Dictionary<EnemyAI, float> _lastHitTime = new Dictionary<EnemyAI, float>();

    void Start()
    {
        // Récupère l’instance PlayerAttack via le tag
        var playerGO = GameObject.FindWithTag("Player");
        if (playerGO == null)
            Debug.LogError("SwordDamage: aucun GameObject taggé 'Player' trouvé.");
        else
            _playerAttack = playerGO.GetComponent<PlayerAttack>();

        // Vérifie qu’on est bien sur un trigger
        var col = GetComponent<Collider>();
        if (!col.isTrigger)
            Debug.LogWarning("SwordDamage: ce collider devrait être en trigger pour la détection de dégâts.");
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("SwordDamage: OnTriggerStay " + other.name);
        // Ne réagit qu’aux monstres
        if (!other.CompareTag("Monster")) return;
        if (!(other.GetComponent<EnemyAI>() is EnemyAI enemy)) return;

        Debug.Log("SwordDamage: " + enemy.name + " touché !");
        // Vérifie le cooldown
        _lastHitTime.TryGetValue(enemy, out float lastTime);
        if (Time.time - lastTime < cooldownPerTarget) return;

        // Récupère les dégâts depuis PlayerAttack
        float dmg = _playerAttack != null
                    ? _playerAttack.AttackDamage
                    : 0f;

        // Inflige les dégâts et mémorise le timestamp
        enemy.TakeDamage(dmg);
        _lastHitTime[enemy] = Time.time;
    }
}
