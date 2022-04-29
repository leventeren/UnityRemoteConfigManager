using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;

public class RemoteConfigsManager : MonoBehaviour
{
    public struct userAttributes {}

    public struct appAttributes {}

    [Header("Fallback Configs Data")]
    public FallbackConfigsData fallbackDefaultConfig;

    void Awake()
    {
        FetchRemoteConfiguration();
        //SetFallbackData();
    }

    public void FetchRemoteConfiguration()
    {
        if(gameObject.active)
        {
            ConfigManager.FetchCompleted += ApplyRemoteSettings;
            ConfigManager.FetchConfigs<userAttributes, appAttributes> (new userAttributes(), new appAttributes());  
            Debug.Log("Fetched Sample");
        }
        
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:

                    Debug.Log("No Setting Loaded this session; using default values");

                    break;

                case ConfigOrigin.Cached:

                    Debug.Log ("No settings loaded this session; using cached values from a previous session.");
                    
                    break;

                case ConfigOrigin.Remote:
                
                    Debug.Log("New Settings loaded this session; update values accordingly");

                    SetEventSettings();
                    SetHeroSettings();
                    SetEnemySettings();
                    SetQualitySettings();

                    break;
        }
    }

    void SetEventSettings()
    {
        bool currentSnowMode = ConfigManager.appConfig.GetBool("eventSettings_snow");

        SnowManager.Instance.SetRemoteConfigurationToSettings(currentSnowMode);
    }

    void SetHeroSettings()
    {
        int heroTotalHealth = ConfigManager.appConfig.GetInt("heroSettings_totalHealth");

        HeroManager.Instance.SetRemoteConfigurationToSettings(heroTotalHealth);
    }

    void SetEnemySettings()
    {
        int enemyWarriorTotalHealth = ConfigManager.appConfig.GetInt("enemySettings_warrior_totalHealth");
        int enemyWarriorSpawnAmount = ConfigManager.appConfig.GetInt("enemySettings_warrior_spawnAmount");

        EnemyManager.Instance.SetRemoteConfigurationToSettings(enemyWarriorSpawnAmount, enemyWarriorTotalHealth);
    }

    void SetQualitySettings()
    {
        bool realtimeShadows = ConfigManager.appConfig.GetBool("graphicsSettings_shadows_realtime");
        bool postProcessing = ConfigManager.appConfig.GetBool("graphicsSettings_postProcessing");

        QualitySettingsManager.Instance.SetRemoteConfigurationToSettings(realtimeShadows, postProcessing);
        
    }
    
    void SetFallbackData()
    {
        SnowManager.Instance.SetRemoteConfigurationToSettings(fallbackDefaultConfig.snow);
        HeroManager.Instance.SetRemoteConfigurationToSettings(fallbackDefaultConfig.heroTotalHealth);
        EnemyManager.Instance.SetRemoteConfigurationToSettings(fallbackDefaultConfig.warriorTotalHealth, fallbackDefaultConfig.warriorSpawnAmount);
        QualitySettingsManager.Instance.SetRemoteConfigurationToSettings(fallbackDefaultConfig.realtimeShadows, fallbackDefaultConfig.postProcessing);
    }
    

}
