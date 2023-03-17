using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("----UI SETTINGS")] 
    public GameObject[] Panels;
    public TextMeshProUGUI StarNumber;
    public TextMeshProUGUI Win_Level_Number;
    public TextMeshProUGUI Lost_Level_Number;


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
            PlayerPrefs.SetInt("Level",SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Star",PlayerPrefs.GetInt("Star")+15);
            StarNumber.text = PlayerPrefs.GetInt("Star").ToString();
            Win_Level_Number.text ="LEVEL : "+ SceneManager.GetActiveScene().name;
            Panels[1].SetActive(true);
        }

        if (MvctBallSys==0 && GrnBallSys!=HdfBallSys)
        {
            Lost_Level_Number.text = "LEVEL : " + SceneManager.GetActiveScene().name;
            Panels[2].SetActive(true);


        }
        if ((MvctBallSys + GrnBallSys) < HdfBallSys)
        {
            Lost_Level_Number.text = "LEVEL : " + SceneManager.GetActiveScene().name;
            Panels[2].SetActive(true);


        }
    }
    public void BallNotEntered()
    {
        if (MvctBallSys==0)
        {
            Lost_Level_Number.text = "LEVEL : " + SceneManager.GetActiveScene().name;

            Panels[2].SetActive(true);

        }

        if ((MvctBallSys+GrnBallSys)<HdfBallSys)
        {
            Lost_Level_Number.text = "LEVEL : " + SceneManager.GetActiveScene().name;
            Panels[2].SetActive(true);

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


    public void PauseGame()
    {
        Panels[0].SetActive(true);
        Time.timeScale = 0;
    }

    public void PanelButtonsProcess(string process)
    {
        switch (process)
        {
            case "Resume": 
                Time.timeScale = 1;
                Panels[0].SetActive(false);
                break;
            case "Exit":
                Application.Quit();
                break;
            case "Settings":
                //Optional
                break;
            case "Retry":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Next":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                break;

        }
    }
}
