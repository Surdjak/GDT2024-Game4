using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Timeline;

[Serializable]
public struct VineInfo
{
    public string Name;

    /// <summary>
    /// The prefab to spawn.
    /// </summary>
    public Vine Prefab;

    /// <summary>
    /// Before being unlocked, vine cannot be planted.
    /// </summary>
    [HideInInspector]
    public bool IsUnlocked;
    public uint UnlockPrice;

    public uint PlantPrice;

    public uint[] ProductionAtLevel;
}
