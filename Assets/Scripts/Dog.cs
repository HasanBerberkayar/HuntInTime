using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
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
            int r = Random.Range(0, 3);
            while(r == ChangeTime.time)
            {
                r = Random.Range(0, 3);
            }
            ChangeTime.time = r;
        }

    }
}
