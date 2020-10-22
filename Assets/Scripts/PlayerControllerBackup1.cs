using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBackup1 : MonoBehaviour
{
    Vector3 origPosition;
    Vector3 position;
    Transform myTransform;

    private float width;
    private float height;

    private float speedModulation = 20f; // Este número es el que va a dividir la velocidad en la que se mueve el personaje

    private float widthMin;
    private float widthMax;
    private float clampedWidthMin;
    private float clampedWidthMax;
    
    public GameObject bulletPrefab;


    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        //height = (float)Screen.height / 3.0f;

        widthMin = - width;
        widthMax = width;
        clampedWidthMin = Mathf.Clamp(widthMin, -2.5f, 2f);
        clampedWidthMax = Mathf.Clamp(widthMax, -2.5f, 2f);

        /*
        Debug.Log("Width: " + width);
        Debug.Log("Min Width: " + widthMin);
        Debug.Log("Max Width: " + widthMax);
        Debug.Log("Min Width Clamped: " + clampedWidthMin);
        Debug.Log("Max Width Clamped: " + clampedWidthMax);
        */
    }

    void Start()
    {
        origPosition = GetComponent<Transform>().position;
        position = new Vector3(origPosition.x, origPosition.y, origPosition.z);
        StartCoroutine("FireBullet");
    }

    void Update()
    {
        StartCoroutine("FireBullet");
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 pos = touch.position;
            pos.x = (pos.x - width) / width;
            position += new Vector3(pos.x/speedModulation, 0.0f, 0.0f);

            // Screen Borders
            if (position.x >= clampedWidthMin && 
                position.x <= clampedWidthMax)
            {
                transform.position = position; // Change position of the object.
            }
        }
    }

    private float fireTime = 0f;
    IEnumerator FireBullet()
    {
        float fireRate = 0.75f;
        fireTime += Time.deltaTime;
        if (fireTime > fireRate)
        {
            fireTime = 0f;            
            Instantiate(bulletPrefab, new Vector2(position.x, position.y), Quaternion.identity); // Fire bullets
        }

        /*
        Debug.Log("Fire Time: " + fireTime);
        Debug.Log("Fire Rate: " + fireRate);
        */

        yield return null;
    }
}
