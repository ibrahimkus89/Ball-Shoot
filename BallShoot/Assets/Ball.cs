using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameManager _gameManager;
    private Rigidbody rb;
    private Renderer _colorr;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _colorr = GetComponent<Renderer>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {
            
            _gameManager.ParcEffect(gameObject.transform.position,_colorr.material.color);

            gameObject.transform.localPosition=Vector3.zero;
            gameObject.transform.localRotation=Quaternion.Euler(Vector3.zero);
            rb.velocity=Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            //parc eff
            //numbers update
            //slider
            _gameManager.BallEntered();
        }
       else if (other.CompareTag("BottomObject"))
        {
            _gameManager.ParcEffect(gameObject.transform.position, _colorr.material.color);


            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            _gameManager.BallNotEntered();
        }
    }
}
