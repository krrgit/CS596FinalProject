using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;

    [SerializeField] private LightingPreset preset;
    [SerializeField] private Vector3 sunOffset = new Vector3( -90f, 170f, 0);

    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField] private WaveSpawner waveSpawner;

    void Update()
    {
        if (preset == null)
        {
            return;
        }
        
        if (Application.isPlaying)
        {
            timeOfDay = 24 * (waveSpawner.DayPercent + 0.25f);
            timeOfDay %= 24;
            UpdateLighting(timeOfDay / 24f);
        }
        
    }
    
    void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);
            float sunPosition = preset.sunPositionCurve.Evaluate(timePercent);
            float xRot = preset.sunXRot.Evaluate(sunPosition);
            float yRot = preset.sunYRot.Evaluate(sunPosition);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(xRot,yRot, 0f)+sunOffset);
        }
    }
    private void OnValidate()
    {
        if (directionalLight != null)
            return;
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                }
            }
        }
    }
}
