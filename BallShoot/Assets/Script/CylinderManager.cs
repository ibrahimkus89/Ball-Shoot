using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CylinderManager : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private bool ButtonPressed;
    public GameObject CylinderObj;
    [SerializeField] private float RotateForce;
    [SerializeField] private string Direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed=true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonPressed =false;
    }


    void Update()
    {
        if (ButtonPressed)
        {
            if (Direction=="Left")
            {
                CylinderObj.transform.Rotate(0, RotateForce * Time.deltaTime, 0, Space.Self);

            }
            else
            {
                CylinderObj.transform.Rotate(0, -RotateForce * Time.deltaTime, 0, Space.Self);

            }

        }
        
    }
}