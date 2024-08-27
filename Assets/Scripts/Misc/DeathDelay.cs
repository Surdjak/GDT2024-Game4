using UnityEngine;

public class DeathDelay : MonoBehaviour
{
    public float TimeBeforeDeath = 2f;

    void Update()
    {
        TimeBeforeDeath -= Time.deltaTime;
        if (TimeBeforeDeath < 0f)
        {
            Destroy(gameObject);
        }
    }
}
