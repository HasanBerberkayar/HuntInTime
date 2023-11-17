using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DogChangeTime();
    }

    public void DogChangeTime()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
            float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);

            if (distanceX <= 1.2 && distanceY <= 1.2)
            {
                int r = Random.Range(0, 3);
                while (r == ChangeTime.time)
                {
                    r = Random.Range(0, 3);
                }
                ChangeTime.time = r;
            }           
        }

    }
}
