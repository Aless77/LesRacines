using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScrip : MonoBehaviour
{
    Light sun;
    public Material daySkybox;
    public Material nightSkybox;

    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        sun=GetComponent<Light>();
        RenderSettings.skybox = daySkybox;
    }

    // Update is called once per frame
    void Update()
    {
        sun.transform.Rotate(Vector3.right * speed * Time.deltaTime);

        if (sun.transform.eulerAngles.x >= 180 && sun.transform.eulerAngles.x < 360)
        {
            RenderSettings.skybox = nightSkybox;
        }
        else
        {
            RenderSettings.skybox = daySkybox;
        }
        DynamicGI.UpdateEnvironment();
    }
}
