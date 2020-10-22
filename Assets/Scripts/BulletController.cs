using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameController gameControllerScript;
    private AudioController audioControllerScript;

    private float speed = 2.5f;

    private float lifeTime = 10f;
    private float beenActiveTime;

    Transform myTransform;
    Vector3 origPosition;

    public GameObject explosionPrefab;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        origPosition = myTransform.position;
        gameControllerScript = GameObject.Find("GameController").GetComponent<GameController>();
        audioControllerScript = GameObject.Find("AudioController").GetComponent<AudioController>();
    }

    void Update()
    {
        myTransform.position += new Vector3(0, speed * Time.deltaTime, 0f);

        beenActiveTime += Time.deltaTime;
        if (beenActiveTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy1"))
        {
            gameControllerScript.playerScore += 10;
            audioControllerScript.bulletExploded = true;
            Destroy(col.gameObject);
            Destroy(this.gameObject);
            Instantiate(explosionPrefab, new Vector2(myTransform.position.x, myTransform.position.y), Quaternion.identity); // Instantiate explosion            
        }
    }

}
