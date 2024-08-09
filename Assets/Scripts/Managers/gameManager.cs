using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PricesManager pricesManager;
    public PossessionsManager possessionsManager;
    public DragAndDropManager dragAndDropManager;

    public float ColliderSize = 0.4f;

    public AnimationData AnimationData;

    public Transform plantingArea;
    private Dictionary<Vine, Vector2> _plantedVinePositions = new Dictionary<Vine, Vector2>();
    private List<Vine> _overlappingVines = new List<Vine>();

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
        if (dragAndDropManager == null)
        {
            Debug.LogError("Missing Drag&Drop Manager!", gameObject);
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
        if (Input.GetMouseButtonDown(0))
        {
            if (ClickedOnPlantedVine(mousePosition, out Vine clickedVine))
            {
                DragVine(clickedVine);
            }
            else if (ClickedOnVineyard(mousePosition))
            {
                PlaceInVineyard(mousePosition);
            }
        }
    }

    private bool ClickedOnPlantedVine(Vector2 mousePosition, out Vine clickedVine)
    {
        clickedVine = null;
        var distances = _plantedVinePositions
            .Where(p => p.Key.IsGrown)
            .ToDictionary(p => Vector2.Distance(mousePosition, p.Value), p => p.Key);

        if (distances.Count == 0)
            return false;

        var closest = distances.Keys.Min();
        if (closest < ColliderSize)
        {
            clickedVine = distances[closest];
            return true;
        }

        return false;
    }

    private bool ClickedOnVineyard(Vector2 mousePosition)
    {
        //TODO: implement check the click happened on the vineyard area and not on a menu
        return true;
    }

    private void PlaceInVineyard(Vector2 position)
    {
        //TODO: place what is selected by the player
        VineInfo selectedVine = pricesManager.Vines[0];

        if (!possessionsManager.TrySpendMoney(selectedVine.PlantPrice, selectedVine.Name))
            return;

        Vine vine = Instantiate(selectedVine.Prefab, position, Quaternion.identity, plantingArea);
        vine.Initialize(selectedVine, possessionsManager, AnimationData);

        _plantedVinePositions.Add(vine, position);
    }

    #region Drag&Drop

    private void DragVine(Vine vineToDrag)
    {
        dragAndDropManager.StartDrag(vineToDrag.gameObject, DropVine, DragVine);
    }

    private void DropVine(GameObject draggedObject, Vector2 mousePosition)
    {
        Vine vine = draggedObject.GetComponent<Vine>();
        _plantedVinePositions[vine] = mousePosition;
        MergeVine(vine, mousePosition);
    }

    private void MergeVine(Vine vine, Vector2 mousePosition)
    {
        while (_overlappingVines.Count >= 3)
        {
            if (_overlappingVines.Count >= 5)
            {
                //TODO: destroy 5 vines, creates 2 with higher level
            }
            else if (_overlappingVines.Count >= 3)
            {
                //TODO: destroy 3 vines, creates 1 with higher level
            }
        }

        _overlappingVines.Clear();
    }

    private void DragVine(GameObject draggedObject, Vector2 mousePosition)
    {
        Vine draggedVine = draggedObject.GetComponent<Vine>();
        if (!draggedVine.CanLevelUp())
            return;

        _overlappingVines = _plantedVinePositions
            .Where(p => p.Key.IsGrown 
                && p.Key.Level == draggedVine.Level
                && Vector2.Distance(mousePosition, p.Value) < ColliderSize)
            .Select(p => p.Key)
            .ToList();

        //TODO: highlight them
    }

    #endregion
}