using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTime : MonoBehaviour
{
    public static int time = 0;
    public GameObject medievalObject;
    public GameObject modernObject;
    public GameObject CyberpunkObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChanceObjectByTime(time);
    }

    public void ChanceObjectByTime(int time)
    {
        if (time == 0)
        {
            medievalObject.SetActive(true);
            modernObject.SetActive(false);
            CyberpunkObject.SetActive(false);
        }
        else if(time == 1)
        {
            medievalObject.SetActive(false);
            modernObject.SetActive(true);
            CyberpunkObject.SetActive(false);
        }
        else
        {
            medievalObject.SetActive(false);
            modernObject.SetActive(false);
            CyberpunkObject.SetActive(true);
        }
    }
   
}
