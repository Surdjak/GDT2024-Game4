using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform plantingArea;
    public PricesManager pricesManager;
    public PossessionsManager possessionsManager;

    private void Start()
    {
        if (plantingArea == null)
        {
            Debug.LogError("Missing Plant Area!", gameObject);
        }
        if (pricesManager == null)
        {
            Debug.LogError("Missing Prices Manager!", gameObject);
        }
        if (possessionsManager == null)
        {
            Debug.LogError("Missing Possessions Manager!", gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (ClickedOnPlantableArea(mousePosition))
            {
                PlaceInVineyard(mousePosition);
            }
        }
    }

    private bool ClickedOnPlantableArea(Vector2 mousePosition)
    {
        return ClickedOnVineyard(mousePosition) && !IsTooCloseToSomethingElse(mousePosition);
    }

    private bool ClickedOnVineyard(Vector2 mousePosition)
    {
        //TODO: implement check the click happened on the vineyard area and not on a menu
        return true;
    }

    private bool IsTooCloseToSomethingElse(Vector2 mousePosition)
    {
        //TODO: implement check we're not too close of another something on the area
        return false;
    }

    private void PlaceInVineyard(Vector2 position)
    {
        //TODO: place what is selected by the player
        VineInfo selectedVine = pricesManager.Vines[0];

        if (!possessionsManager.HasEnoughMoney(selectedVine.PlantPrice))
        {
            //TODO: display not enough money
            return;
        }

        possessionsManager.SpendMoney(selectedVine.PlantPrice);
        Vine vine = Instantiate(selectedVine.Prefab, position, Quaternion.identity, plantingArea);
        vine.Initialize(possessionsManager);
    }
}