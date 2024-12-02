using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverSoundController : MonoBehaviour
{
    public MeshCollider riverCollider; // Assignez votre collider de rivière ici
    public float maxVolume = 1.0f; // Le volume maximum de l'audioSource
    public float maxDistance = 50f; // La distance maximale à laquelle on entend encore le son
    public Transform player; // Le joueur

    private AudioSource audioSource;

    bool volumeIsUpdating = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        riverCollider = GetComponent<MeshCollider>(); // Obtenir le collider de la rivière sur le même GameObject que ce script
        // Objet qui s'appelle "Player" dans la scène
        player = GameObject.Find("Player").transform;
        audioSource.volume = 0; // Commencez à volume 0 et ajustez en fonction de la position du joueur

    }

    void Update()
    {
        if (!volumeIsUpdating)
        {
            StartCoroutine(UpdateVolume());
        }
    }

    IEnumerator UpdateVolume()
    {
        volumeIsUpdating = true;
        // Get tout les points du mesh collider
        Vector3[] points = riverCollider.sharedMesh.vertices;

        // Conversion des points en Vector3 du monde
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = riverCollider.transform.TransformPoint(points[i]);
        }

        // Search le point le plus proche du joueur
        float closestDistance = Mathf.Infinity;
        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(player.position, point);
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }
        }

        // Calculez le volume basé sur la distance la plus proche
        float volume = Mathf.Clamp01(1 - (closestDistance / maxDistance)) * maxVolume;
        if (volume > 0.65) // Si le volume est supérieur à 0.65, on le règle à 0.65
            volume = 0.65f;
        audioSource.volume = volume;
        yield return new WaitForSeconds(1f);
        volumeIsUpdating = false;
    }
}
