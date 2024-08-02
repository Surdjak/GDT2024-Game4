using System.Collections.Generic;
using UnityEngine;

public class PricesManager : MonoBehaviour
{
    public List<VineInfo> Vines = new List<VineInfo>();
    public List<BuildingInfo> Buildings = new List<BuildingInfo>();

    void Start()
    {
        ValidateAndInitializeInfos();
    }

    private void ValidateAndInitializeInfos()
    {
        for (int i=0; i < Vines.Count; i++)
        {
            VineInfo info = Vines[i];
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

            if (info.UnlockPrice == 0)
                info.IsUnlocked = true;
        }
    }
}
