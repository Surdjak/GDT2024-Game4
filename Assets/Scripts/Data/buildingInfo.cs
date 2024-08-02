using System;
using UnityEngine;

[Serializable]
public struct BuildingInfo
{
    public GameObject Prefab;

    public uint UnlockPrice;
    public bool IsUnlocked;

    public uint BuildPrice;

    public uint CurrentLevel;
    public uint[] UpgradePrices;
}