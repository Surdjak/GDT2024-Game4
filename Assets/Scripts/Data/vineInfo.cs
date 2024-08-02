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

    /// <summary>
    /// Current upgrade level.
    /// </summary>
    [HideInInspector]
    public uint CurrentLevel;
    /// <summary>
    /// Read as UpgradePrices[<see cref="CurrentLevel"/>], it means "the price you have to pay to upgrade to next level".
    /// </summary>
    public uint[] UpgradePrices;
    public uint[] ProductionAtLevel;
}
