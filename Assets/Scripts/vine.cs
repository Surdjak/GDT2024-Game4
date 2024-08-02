using UnityEngine;

public class Vine : MonoBehaviour
{
    public Sprite seedSprite;
    public Sprite growingSprite;
    public Sprite grownSprite;
    public float growthTime = 10.0f;

    private PossessionsManager possessionsManager;

    private SpriteRenderer spriteRenderer;
    private float growthProgress = 0.0f;
    private bool isGrown = false;

    public void Initialize(PossessionsManager possessionsManager)
    {
        this.possessionsManager = possessionsManager;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing!", gameObject);
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
        Debug.Log("Wine Produced!", gameObject);
    }

    public bool IsGrown()
    {
        return isGrown;
    }
}