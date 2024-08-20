using UnityEngine;

public class Vine : MonoBehaviour
{
    public Sprite seedSprite;
    public Sprite growingSprite;
    public Sprite grownSprite;
    public float growthTime = 10.0f;
    public float productionTime = 2f;

    private PossessionsManager possessionsManager;
    private AnimationData animationData;
    private VineInfo infos;

    private SpriteRenderer spriteRenderer;
    private float growthProgress = 0.0f;
    private bool isGrown = false;
    private int level = 0;

    private float productionProgress = 0.0f;
    private ScaleAnimator productionSqueezeAnimator;
    private FloatingText productionFloatingTextAnimator;

    public void Initialize(VineInfo infos, PossessionsManager possessionsManager, AnimationData animationData)
    {
        this.possessionsManager = possessionsManager;
        this.animationData = animationData;
        this.infos = infos;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = seedSprite;

        productionSqueezeAnimator = gameObject.AddComponent<ScaleAnimator>();
        productionSqueezeAnimator.Curve = animationData.VineProductionScaleAnimationCurve;

        productionFloatingTextAnimator = gameObject.AddComponent<FloatingText>();
        productionFloatingTextAnimator.TextMeshPrefab = animationData.VineProductionFloatingTextPrefab;
        productionFloatingTextAnimator.Direction = animationData.VineProductionFloatingTextDirection;
        productionFloatingTextAnimator.Speed = animationData.VineProductionFloatingTextSpeed;
        productionFloatingTextAnimator.Duration = animationData.VineProductionFloatingTextDuration;
    }

    public bool IsGrown => isGrown;
    public int Level => level;

    public bool CanLevelUp()
    {
        return infos.ProductionAtLevel.Length > level + 1;
    }

    void Start()
    {
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

    private void Produce()
    {
        productionProgress += Time.deltaTime;
        if (productionProgress > productionTime)
        {
            var productionValue = infos.ProductionAtLevel[Level];
            possessionsManager.GainGrapes(productionValue);
            productionProgress -= productionTime;
            productionSqueezeAnimator.Animate();
            productionFloatingTextAnimator.Animate($"+{productionValue}");
        }
    }

    private void Grow(bool growNow = false)
    {
        growthProgress += Time.deltaTime;
        if (growNow || growthProgress > growthTime)
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

    /// <summary>
    /// Creates a Vine of higher level and initializes it with same infos as the original
    /// </summary>
    /// <param name="position"></param>
    /// <param name="offset">Offsets horizontally from given <paramref name="position"/> ; default 0 is no offset</param>
    /// <returns></returns>
    public Vine CreateHigherVine(Vector2 position, float offset = 0f)
    {
        var truePosition = new Vector2(position.x + offset, position.y);
        Vine newVine = Instantiate(infos.Prefab, truePosition, Quaternion.identity, transform.parent);
        newVine.Initialize(infos, possessionsManager, animationData);
        newVine.level = level + 1;
        newVine.Grow(true);
        return newVine;
    }
}