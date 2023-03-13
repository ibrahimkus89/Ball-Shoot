using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Ball;
    public GameObject FirePoint;
    public float BallForce;
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject tp = Instantiate(Ball,FirePoint.transform.position,FirePoint.transform.rotation);

            tp.GetComponent<Rigidbody>().AddForce(tp.transform.TransformDirection(90,90,0) * BallForce,ForceMode.Force);
        }
    }
}
