using UnityEngine;

public class UnderwaterFogController : MonoBehaviour
{
    public float waterLevel = 0.0f;
    public Material underwaterFogMaterial;
    public float fogDensityAboveWater = 0.01f;
    public float fogDensityUnderWater = 0.05f;

    private void Update()
    {
        if (transform.position.y < waterLevel)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = underwaterFogMaterial.GetColor("_FogColor");
            RenderSettings.fogDensity = fogDensityUnderWater;
        }
        else
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.white; // Default fog color above water
            RenderSettings.fogDensity = fogDensityAboveWater;
        }
    }
}
