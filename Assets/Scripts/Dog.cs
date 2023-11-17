using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Creature
{
    public GameObject player;
    float distanceX;
    float distanceY;
    bool isNear = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);

        DogChangeTime();
        FollowPlayer();
    }

    public void DogChangeTime()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {           
            if (distanceX <= 1.2 && distanceY <= 1.2)
            {
                int r = UnityEngine.Random.Range(0, 3);
                while (r == ChangeTime.time)
                {
                    r = UnityEngine.Random.Range(0, 3);
                }
                ChangeTime.time = r;
            }           
        }
    }

    public void FollowPlayer()
    {
        if (distanceX > 3.5)
        {
            if(player.transform.position.x - transform.position.x >= 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            Move(direction);
            isNear = true;
        }
        else if (isNear)
        {
            if (player.transform.position.x - transform.position.x >= 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            Move(direction);

            if (distanceX < 1.4)
            {
                isNear = false;
            }
        }
        else
        {
            rb2D.velocity = Vector3.zero;
        }
    }
}
