using UnityEngine;

public class Plant : MonoBehaviour
{
    public Sprite seedSprite;
    public Sprite growingSprite;
    public Sprite grownSprite;

    private SpriteRenderer spriteRenderer;
    private float growthTime = 10.0f; // Adjust growth time as needed
    private float growthProgress = 0.0f;
    private bool isGrown = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing!");
            return;
        }
        spriteRenderer.sprite = seedSprite;
    }

    void Update()
    {
        if (!isGrown)
        {
            growthProgress += Time.deltaTime;
            if (growthProgress >= growthTime)
            {
                spriteRenderer.sprite = grownSprite;
                isGrown = true;
                ProduceWine();
            }
            else if (growthProgress >= growthTime / 2)
            {
                spriteRenderer.sprite = growingSprite;
            }
        }
    }

    void ProduceWine()
    {
        // Implement wine production logic here
        Debug.Log("Wine Produced!");
    }

    public bool IsGrown()
    {
        return isGrown;
    }
}