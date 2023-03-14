using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {
            gameObject.transform.localPosition=Vector3.zero;
            gameObject.transform.localRotation=Quaternion.Euler(Vector3.zero);
            gameObject.SetActive(false);
            //parc eff
            //numbers update
            //slider
        }
       else if (other.CompareTag("BottomObject"))
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            gameObject.SetActive(false);
        }
    }
}
