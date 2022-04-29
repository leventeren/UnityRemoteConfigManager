using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class QualitySettingsManager : Singleton<QualitySettingsManager>
{

    public UniversalAdditionalCameraData universalAdditionalCameraData;

    private bool realtimeShadowsEnabled = true;
    private bool postProcessingEnabled = true;
   
   public void SetRemoteConfigurationToSettings(bool realtimeShadowsNewState, bool postProcessingNewState)
   {
       realtimeShadowsEnabled = realtimeShadowsNewState;
       postProcessingEnabled = postProcessingNewState;

       SetQualitySettingsToGameCamera();
   }

    void SetQualitySettingsToGameCamera()
    {
        universalAdditionalCameraData.renderShadows = realtimeShadowsEnabled;
        universalAdditionalCameraData.renderPostProcessing = postProcessingEnabled;
    }

}
