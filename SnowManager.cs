using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SnowManager : Singleton<SnowManager>
{

    [Header("Debug Setting")]
    private bool snowMode = false;

    private bool currentSnowMode;

    [Header("Main Shaders")]
    public Shader mainLitShader;
    public Shader snowLayerShader;

    [Header("Main Materials")]
    public Material[] materialsToSwitch;

    [Header("Floor Settings")]
    public MeshRenderer floorRenderer;
    public Material floorDetailGrassMat;
    public Material floorDetailSnowMat;

    [Header("Grass Settings")]
    public Material grassMaterial;
    public Shader mainGrassShader;
    public Shader snowGrassShader;

    [Header("Snow Particles Settings")]
    public GameObject snowParticles;
    private bool currentParticlesState;

    [Header("Global Snow Layer Shader Controls")]
    public Color snowColor;
    public float snowEdge1;
    public float snowEdge2;
    [Range(0, 1)] public float snowFade;

    private Color currentSnowColor;
    private float currentSnowEdge1;
    private float currentShowEdge2;
    private float currentSnowFade;

    private int snowColorID;
    private int snowEdge1ID;
    private int snowEdge2ID;
    private int snowFadeID;

    private bool shadersNeedToBeUpdated;


    void OnEnable()
    {
        SetupShaderIDs();

    }

    void SetupShaderIDs()
    {
        snowColorID = Shader.PropertyToID("_Snow_Color");
        snowEdge1ID = Shader.PropertyToID("_Snow_Edge_1");
        snowEdge2ID = Shader.PropertyToID("_Snow_Edge_2");
        snowFadeID = Shader.PropertyToID("_Snow_Fade");

        StoreValues();
    }

    void StoreValues()
    {

        currentSnowMode = snowMode;

        currentSnowColor = snowColor;
        currentSnowEdge1 = snowEdge1;
        currentShowEdge2 = snowEdge2;
        currentSnowFade = snowFade;

        shadersNeedToBeUpdated = true;

        UpdateMaterials();
        UpdateParticles();
        UpdateAllShaderValues();
    }

    public void SetRemoteConfigurationToSettings(bool newState)
    {
        snowMode = newState;
    }

    void Update()
    {
        if(currentSnowMode != snowMode)
        {
            currentSnowMode = snowMode;
            UpdateMaterials();
            UpdateParticles();
        }

        if(currentSnowColor != snowColor)
        {
            currentSnowColor = snowColor;
            shadersNeedToBeUpdated = true;
        }

        if(currentSnowEdge1 != snowEdge1)
        {
            currentSnowEdge1 = snowEdge1;
            shadersNeedToBeUpdated = true;
        }

        if(currentShowEdge2 != snowEdge2)
        {
            currentShowEdge2 = snowEdge2;
            shadersNeedToBeUpdated = true;
        }

        if(currentSnowFade != snowFade)
        {
            currentSnowFade = snowFade;
            shadersNeedToBeUpdated = true;
        }

        if(shadersNeedToBeUpdated)
        {
            UpdateAllShaderValues();
        }
    }

    Shader newShader;

    void UpdateMaterials()
    {

        if(currentSnowMode == true)
        {
            newShader = snowLayerShader;

            floorRenderer.material = floorDetailSnowMat;
            grassMaterial.shader = snowGrassShader;

        }
        else if(currentSnowMode == false)
        {
            newShader = mainLitShader;

            floorRenderer.material = floorDetailGrassMat;
            grassMaterial.shader = mainGrassShader;
        }

        for(int i = 0; i < materialsToSwitch.Length; i++)
        {
            materialsToSwitch[i].shader = newShader;
        }



        UpdateAllShaderValues();
    }

    void UpdateAllShaderValues()
    {
       Shader.SetGlobalColor(snowColorID, currentSnowColor);
       Shader.SetGlobalFloat(snowEdge1ID, currentSnowEdge1);
       Shader.SetGlobalFloat(snowEdge2ID, currentShowEdge2);
       Shader.SetGlobalFloat(snowFadeID, currentSnowFade);

       shadersNeedToBeUpdated = false;

    }

    void UpdateParticles()
    {
        snowParticles.SetActive(currentSnowMode);
    }


}
