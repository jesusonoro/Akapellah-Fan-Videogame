using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 0.35f;

    Transform myTransform;
    Vector3 origPosition;
    public GameObject explosionPrefab;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        origPosition = myTransform.position;
    }

    void Update()
    {
        myTransform.position += new Vector3(0, (-origPosition.y * speed * Time.deltaTime), 0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Instantiate(explosionPrefab, new Vector2(myTransform.position.x, myTransform.position.y), Quaternion.identity); // Instantiate explosion            
        }
    }
}