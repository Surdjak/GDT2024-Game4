using UnityEngine;

public class Vine : MonoBehaviour
{
    public Sprite seedSprite;
    public Sprite growingSprite;
    public Sprite grownSprite;
    public float growthTime = 10.0f;
    public float productionTime = 2f;

    private PossessionsManager possessionsManager;
    private VineInfo infos;

    private SpriteRenderer spriteRenderer;
    private float growthProgress = 0.0f;
    private bool isGrown = false;

    private float productionProgress = 0.0f;

    public void Initialize(VineInfo infos, PossessionsManager possessionsManager)
    {
        this.possessionsManager = possessionsManager;
        this.infos = infos;
    }

    public bool IsGrown => isGrown;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing!", gameObject);
            return;
        }
        spriteRenderer.sprite = seedSprite;

        // hack to have the vine produce when it's just grown
        productionProgress = productionTime + 0.1f;
    }

    void Update()
    {
        if (isGrown)
        {
            Produce();
        }
        else
        {
            Grow();
        }
    }

    void Produce()
    {
        productionProgress += Time.deltaTime;
        if (productionProgress > productionTime)
        {
            possessionsManager.GainGrapes(infos.ProductionAtLevel[infos.CurrentLevel]);
            productionProgress -= productionTime;
        }
    }

    void Grow()
    {
        growthProgress += Time.deltaTime;
        if (growthProgress > growthTime)
        {
            spriteRenderer.sprite = grownSprite;
            isGrown = true;
            Produce();
        }
        else if (growthProgress > growthTime / 2)
        {
            spriteRenderer.sprite = growingSprite;
        }
    }
}