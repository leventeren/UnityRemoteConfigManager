using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : Singleton<HeroManager>
{
    [Header("Hero References")]
    public Transform heroTransform;
    public HeroBehaviour heroBehaviour;

    [Header("Debug Follow Guide References")]
    public Transform followSphere;

    public void SetRemoteConfigurationToSettings(int newHeroTotalHealth)
    {
        heroBehaviour.SetNewTotalHealth(newHeroTotalHealth);
        GameUIManager.Instance.SetPlayerTotalHealthUI(newHeroTotalHealth);

    }

    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        followSphere.position = targetPosition;
        heroBehaviour.MoveToTargetPosition(targetPosition);
    }

    public void StartAttackingTargetEnemy(GameObject targetEnemy)
    {
        heroBehaviour.StartAttackingTargetEnemy(targetEnemy);
    }
    
}
