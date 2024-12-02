using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{

    public GameObject[] enemiesToSpawn;
    private List<GameObject> enemies = new List<GameObject>();
    public int maxEnemies = 5;
    public float delayBetweenSpawns = 60f;
    private float lastSpawnTime;

    public Vector3 sizeZone;
    private Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        // R�cup�r� les positions de tous les enfants de la zone de spawn
        spawnPoints = new Transform[transform.childCount]; 
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }
        lastSpawnTime = 0;
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawnTime += Time.deltaTime; // On incr�mente le temps �coul� depuis le dernier spawn
        if (lastSpawnTime >= delayBetweenSpawns) // Si le temps �coul� est sup�rieur au temps entre chaque spawn
        {
            lastSpawnTime = 0;
            SpawnEnemy(); // On spawn un ennemi
        }

    }

    private void SpawnEnemy()
    {
        if (enemies.Count < maxEnemies) // Si le nombre d'ennemis est inf�rieur au nombre maximum d'ennemis
        {
            int randomIndex = Random.Range(0, enemiesToSpawn.Length-1); // On choisi un ennemi al�atoire
            int randomIndexSpawn = Random.Range(0, spawnPoints.Length-1); // On choisi un point de spawn al�atoire
            GameObject enemy = Instantiate(enemiesToSpawn[randomIndex], spawnPoints[randomIndexSpawn].position, Quaternion.identity); // On instancie l'ennemi
            enemies.Add(enemy); // On ajoute l'ennemi � la liste des ennemis
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, sizeZone);
    }
}
