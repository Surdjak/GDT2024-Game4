using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PricesManager pricesManager;
    public PossessionsManager possessionsManager;

    public AnimationData AnimationData;

    public Transform plantingArea;
    private Dictionary<Vine, Vector2> plantedVinePositions = new Dictionary<Vine, Vector2>();

    private Vine _draggedVine;

    private void Start()
    {
        if (pricesManager == null)
        {
            Debug.LogError("Missing Prices Manager!", gameObject);
        }
        if (possessionsManager == null)
        {
            Debug.LogError("Missing Possessions Manager!", gameObject);
        }
        if (AnimationData == null)
        {
            Debug.LogError("Missing Animation Data!", gameObject);
        }
        if (plantingArea == null)
        {
            Debug.LogError("Missing Plant Area!", gameObject);
        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonUp(0) && _draggedVine != null)
        {
            DropVine(mousePosition);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (ClickedOnPlantedVine(mousePosition, out Vine clickedVine))
            {
                // start drag
                _draggedVine = clickedVine;
            }
            else if (ClickedOnPlantableArea(mousePosition))
            {
                PlaceInVineyard(mousePosition);
            }
        }
        else if (_draggedVine != null)
        {
            DragVine(mousePosition);
        }
    }

    private bool ClickedOnPlantedVine(Vector2 mousePosition, out Vine clickedVine)
    {
        clickedVine = null;
        var distances = plantedVinePositions
            .Where(p => p.Key.IsGrown)
            .ToDictionary(p => Vector2.Distance(mousePosition, p.Value), p => p.Key);

        if (distances.Count == 0)
            return false;

        var closest = distances.Keys.Min();
        if (closest < 0.4f)
        {
            clickedVine = distances[closest];
            return true;
        }

        return false;
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

        if (!possessionsManager.TrySpendMoney(selectedVine.PlantPrice, selectedVine.Name))
            return;

        Vine vine = Instantiate(selectedVine.Prefab, position, Quaternion.identity, plantingArea);
        vine.Initialize(selectedVine, possessionsManager, AnimationData);

        plantedVinePositions.Add(vine, position);
    }

    #region Drag&Drop

    private void DropVine(Vector2 mousePosition)
    {
        plantedVinePositions[_draggedVine] = mousePosition;
        _draggedVine = null;
    }

    private void DragVine(Vector2 mousePosition)
    {
        _draggedVine.transform.position = mousePosition;
    }

    #endregion
}