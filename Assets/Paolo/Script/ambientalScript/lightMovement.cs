using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightMovement : MonoBehaviour
{
    public GameObject luce;
    public float sec, x, y, z;

    Coroutine lightCorutine;

    // Start is called before the first frame update
    void Start()
    {
        lightCorutine = StartCoroutine(lightCounter(sec));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator lightCounter(float second)
    {
        while (true)
        {
            luce.transform.position += new Vector3(x, y, z); 
            yield return new WaitForSeconds(second);
            luce.transform.position -= new Vector3(x, y, z);
        }
    }
}
