using System.Collections.Generic;
using UnityEngine;

public class PricesManager : MonoBehaviour
{
    // Ugly way to define everything while we don't have a custom editor to fill dictionaries
    public Plant WhiteGrapePrefab;
    public uint WhiteGrapePrice = 100;

    public List<VineInfo> Vines = new List<VineInfo>();
    public List<BuildingInfo> Buildings = new List<BuildingInfo>();

    void Start()
    {
        UglyFill();
        ValidateInfos();
    }

    private void UglyFill()
    {
        Vines.Add(new VineInfo
        {
            Prefab = WhiteGrapePrefab,
            IsUnlocked = true,
            PlantPrice = WhiteGrapePrice,
            UpgradePrices = new uint[0], // no upgrade for now
            ProductionAtLevel = new uint[1] { 2 }
        });
    }

    private void ValidateInfos()
    {
        foreach (var info in Vines)
        {
            if (info.Prefab == null)
            {
                Debug.LogError("Vine with no prefab!", gameObject);
            }
            if (info.UpgradePrices == null)
            {
                Debug.LogError("Null UpdatePrices array!", gameObject);
            }
            if (info.ProductionAtLevel == null)
            {
                Debug.LogError("Null ProductionAtLevel array!", gameObject);
            }
            if (info.UpgradePrices != null && info.ProductionAtLevel != null && info.ProductionAtLevel.Length != info.UpgradePrices.Length + 1)
            {
                Debug.LogError("There should be one more ProductionAtLevel than UpdatePrices!", gameObject);
            }
        }
    }
}
