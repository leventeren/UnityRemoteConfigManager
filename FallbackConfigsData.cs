using UnityEngine;

[CreateAssetMenu(fileName = "Fallback Config Data", menuName = "Archer Rush/Fallback Config Data", order = 1)]
public class FallbackConfigsData : ScriptableObject
{
    
    [Header("Event Settings")]
    public bool snow;

    [Header("Hero Settings")]
    public int heroTotalHealth;
    public int heroAttackDamage;

    [Header("Enemy Settings")]
    public int warriorTotalHealth;
    public float warriorSpeed;
    public int warriorSpawnAmount;

    [Header("Graphics Settings")]
    public bool realtimeShadows;
    public bool postProcessing;


}
