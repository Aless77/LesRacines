using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScrip : MonoBehaviour
{
    Light sun;
    public Material daySkybox;
    public Material nightSkybox;

    public float speed = 1f;

    [Header("Light Intensity Settings")]
    public float maxIntensity = 1.2f; // Intensité maximale (jour)
    public float minIntensity = 0.2f; // Intensité minimale (nuit)

    // Start is called before the first frame update
    void Start()
    {
        sun = GetComponent<Light>();
        RenderSettings.skybox = daySkybox;
        sun.intensity = maxIntensity; // Initialiser l'intensité à sa valeur maximale
    }

    // Update is called once per frame
    void Update()
    {
        // Faire pivoter le soleil
        sun.transform.Rotate(Vector3.right * speed * Time.deltaTime);

        // Contrôler le changement de skybox
        if (sun.transform.eulerAngles.x >= 180 && sun.transform.eulerAngles.x < 360)
        {
            RenderSettings.skybox = nightSkybox;
        }
        else
        {
            RenderSettings.skybox = daySkybox;
        }

        // Ajuster l'intensité lumineuse
        AdjustLightIntensity();

        // Mettre à jour l'éclairage global pour refléter les changements
        DynamicGI.UpdateEnvironment();
    }

    void AdjustLightIntensity()
    {
        // Calcule un facteur basé sur l'angle X de la lumière directionnelle
        float angle = sun.transform.eulerAngles.x;

        if (angle <= 90) // Lever du soleil (matin)
        {
            sun.intensity = Mathf.Lerp(minIntensity, maxIntensity, angle / 90f);
        }
        else if (angle >= 270) // Coucher du soleil (soir)
        {
            sun.intensity = Mathf.Lerp(maxIntensity, minIntensity, (angle - 270) / 90f);
        }
        else if (angle > 90 && angle < 270) // Nuit
        {
            sun.intensity = minIntensity;
        }
    }
}
