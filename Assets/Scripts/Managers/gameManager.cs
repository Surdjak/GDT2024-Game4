using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Plant plantPrefab;
    public Transform plantingArea;
    public PossessionsManager possessionsManager;

    private void Start()
    {
        if (plantPrefab == null)
        {
            Debug.LogError("Missing Plant Prefab!", gameObject);
        }
        if (plantingArea == null)
        {
            Debug.LogError("Missing Plant Area", gameObject);
        }
        if (possessionsManager == null)
        {
            Debug.LogError("Missing Vineyard Resources Manager!", gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Plant plant = Instantiate(plantPrefab, mousePosition, Quaternion.identity, plantingArea);
            plant.Initialize(possessionsManager);
        }
    }
}