using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Plant plantPrefab;
    public Transform plantingArea;
    public Text resourceText;

    private int wineCount = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Plant plant = Instantiate(plantPrefab, mousePosition, Quaternion.identity, plantingArea);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            if (hitCollider != null)
            {
                Plant plant = hitCollider.GetComponent<Plant>();
                if (plant != null && plant.IsGrown())
                {
                    Destroy(hitCollider.gameObject);
                    wineCount++;
                    UpdateResourceText();
                }
            }
        }
    }

    void UpdateResourceText()
    {
        resourceText.text = "Wine Bottles: " + wineCount;
    }
}