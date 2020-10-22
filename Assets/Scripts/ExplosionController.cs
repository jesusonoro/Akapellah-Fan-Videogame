using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private float lifeTime = 0.8f;
    private float beenActiveTime;

    void Awake()
    {
        beenActiveTime = 0f;
    }

    void Update()
    {
        beenActiveTime += Time.deltaTime;
        if (beenActiveTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
