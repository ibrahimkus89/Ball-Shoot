using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("----BALL SETTINGS")]
    public GameObject[] Balls;
    public GameObject FirePoint;
    [SerializeField] private float BallForce;
    private int ActiveBallIndex;

    [Header("----LEVEL SETTINGS")]
    [SerializeField]private int HdfBallSys;
    [SerializeField] private int MvctBallSys;
    private int GrnBallSys;
    public Slider LevelSlider;
    public TextMeshProUGUI KlnBallSys_Text;
    void Start()
    {
        LevelSlider.maxValue = HdfBallSys;
        KlnBallSys_Text.text = MvctBallSys.ToString();
    }

    public void BallEntered()
    {
        GrnBallSys++;
        LevelSlider.value = GrnBallSys;
        if (GrnBallSys==HdfBallSys)
        {
            // ball lock
            Debug.Log("Win");
        }

        if (MvctBallSys==0 && GrnBallSys!=HdfBallSys)
        {
            Debug.Log("Lost");

        }
        if ((MvctBallSys + GrnBallSys) < HdfBallSys)
        {
            Debug.Log("Lost");

        }
    }
    public void BallNotEntered()
    {
        if (MvctBallSys==0)
        {
            Debug.Log("Lost");

        }

        if ((MvctBallSys+GrnBallSys)<HdfBallSys)
        {
            Debug.Log("Lost");
            
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            MvctBallSys--;
            KlnBallSys_Text.text = MvctBallSys.ToString();
            Balls[ActiveBallIndex].transform.SetPositionAndRotation( FirePoint.transform.position, FirePoint.transform.rotation);
            Balls[ActiveBallIndex].SetActive(true);
            Balls[ActiveBallIndex].GetComponent<Rigidbody>().AddForce(Balls[ActiveBallIndex].transform.TransformDirection(90,90,0) * BallForce,ForceMode.Force);

            if (Balls.Length -1 ==ActiveBallIndex)
            {
                ActiveBallIndex = 0;
            }
            else
            {
                ActiveBallIndex++;

            }
        }
    }
}
