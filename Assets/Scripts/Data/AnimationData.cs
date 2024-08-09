using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AnimationData")]
public class AnimationData : ScriptableObject
{
    // Vine production
    [Header("Vine Production - Scale")]
    [Label("Animation Curve")]
    public AnimationCurve VineProductionScaleAnimationCurve;

    [Header("Vine Production - Floating Text")]
    [Label("Prefab")]
    public GameObject VineProductionFloatingTextPrefab;
    [Label("Direction")]
    public Vector3 VineProductionFloatingTextDirection;
    [Label("Speed")]
    public float VineProductionFloatingTextSpeed;
    [Label("Duration")]
    public float VineProductionFloatingTextDuration;
}