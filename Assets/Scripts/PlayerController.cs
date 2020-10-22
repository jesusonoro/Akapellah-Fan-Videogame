using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Transform myTransform;

    private float width;
    private float height;

    private float widthMin;
    private float widthMax;
    private float clampedWidthMin;
    private float clampedWidthMax;

    private float fireTime;
    private float fireRate = 0.3f;
    public GameObject bulletPrefab;
    public TextMesh playerDebugText;

    private Touch touch1;
    private Touch touch2;
    private bool firstTouch;
    private Vector3 touch1Vector1, touch1Vector2;
    private Vector3 speed;
    public GameObject joy, stick;

    private int state;

    /* STATE MACHINE
     * 0: Not pressing
     * 1: Joystick still pressed
     * 2: Shooting still pressed
     */

    void Awake()
    {
        state = 0;
        fireTime = 0f;
        firstTouch = false;
        width = (float)Screen.width;
        height = (float)Screen.height;
        speed = new Vector3(0, 0, 0);
        touch1Vector1 = new Vector3(0, 0, 0);
        touch1Vector2 = new Vector3(0, 0, 0);
    }

    void Start()
    {
        joy.SetActive(false);
    }

    void Update()
    {
        try
        {
            ProcessInput();
        }
        catch(Exception e)
        {
            playerDebugText.text = e.ToString();
        }
    }

    void ProcessInput()
    {
        switch (Input.touchCount)
        {
            case 0:
                if (firstTouch)
                {
                    joy.SetActive(false);
                    firstTouch = false;
                    state = 0;
                }
                break;

            case 1:
                touch1 = Input.GetTouch(0);
                Vector2 pos = touch1.position;
                ProcessJoystick(pos);
                if (state == 2)
                {
                    FireBullet();
                }
                playerDebugText.text = pos.ToString() + " " + state.ToString();
                break;

            case 2:
                touch1 = Input.GetTouch(0);
                touch2 = Input.GetTouch(1);
                Vector2 pos1 = touch1.position;
                Vector2 pos2 = touch2.position;

                if (pos1.x < 2 * width / 3)
                {
                    ProcessJoystick(pos1);

                    if (pos2.x > 2 * width / 3)
                    {
                        playerDebugText.text = "First moving then shooting";
                        FireBullet();
                    }
                }
                else
                {
                    if (pos2.x < 2 * width / 3)
                    {
                        playerDebugText.text = "First shooting then moving";
                        ProcessJoystick(pos2);
                    }

                    FireBullet();
                }
                break;
        }
    }

    void FireBullet()
    {
        fireTime += Time.deltaTime;
        if (fireTime > fireRate)
        {
            Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity); // Fire bullets
            fireTime = 0f;
        }
    }

    void ProcessJoystick(Vector2 vec)
    {
        if (!firstTouch)
        {
            // First finger pressed
            firstTouch = true;
            if (vec.x < 2 * width / 3)
            {
                // Touching to move
                state = 1;
                joy.SetActive(true);
                touch1Vector1 = new Vector3(2.8f * (vec.x - (width / 2)) / (width / 2), 5f * (vec.y - (height / 2)) / (height / 2), 0);
                joy.GetComponent<Transform>().position = touch1Vector1;
            }
            else
            {
                // Touching to shoot
                state = 2;
            }
        }

        if (state == 1)
        {
            if (vec.x < 2 * width / 3)
            {
                touch1Vector2 = new Vector3(2.8f * (vec.x - (width / 2)) / (width / 2), 5f * (vec.y - (height / 2)) / (height / 2), 0);

                if ((touch1Vector2 - touch1Vector1).sqrMagnitude > 1)
                {
                    stick.GetComponent<Transform>().position = touch1Vector1 + Vector3.Normalize(touch1Vector2 - touch1Vector1);
                }
                else
                {
                    stick.GetComponent<Transform>().position = touch1Vector2;
                }
                // Move player
                float mox = 0.03f * (touch1Vector2.x - touch1Vector1.x);
                float moy = 0.03f * (touch1Vector2.y - touch1Vector1.y);

                if (transform.position.x + mox > 2f || transform.position.x + mox < -2f)
                {
                    mox = 0;
                }
                if (transform.position.y + moy > 4.3f || transform.position.y + moy < -4.3f)
                {
                    moy = 0;
                }
                transform.position = transform.position + new Vector3(mox, moy, 0);
            }
            else
            {
                firstTouch = false;
                joy.SetActive(false);
                state = 2;
            }
        }
    }
}
