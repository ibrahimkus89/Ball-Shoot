using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("----BALL SETTINGS")]
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject FirePoint;
    [SerializeField] private float BallForce;
    private int ActiveBallIndex;
    [SerializeField] private Animator _Ball_Animator;
    [SerializeField] private ParticleSystem _Ball_Throw_Effect;
    [SerializeField] private ParticleSystem[] BallEffects;
    private int ActiveBallEffectIndex;
    [SerializeField] private AudioSource[] BallSounds;
    private int ActiveBallSoundIndex;

    [Header("----LEVEL SETTINGS")]
    [SerializeField]private int HdfBallSys;
    [SerializeField] private int MvctBallSys;
    private int GrnBallSys;
    [SerializeField] private Slider LevelSlider;
    [SerializeField] private TextMeshProUGUI KlnBallSys_Text;

    [Header("----UI SETTINGS")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI StarNumber;
    [SerializeField] private TextMeshProUGUI Win_Level_Number;
    [SerializeField] private TextMeshProUGUI Lost_Level_Number;

    [Header("----OTHER SETTINGS")]
    [SerializeField] private Renderer BucketTransparent; 
    [SerializeField] private AudioSource[] Other_Sound;
    private string Level_Name;


    private float Bucket_st_value;
    private float Bucket_Step_value;
    void Start()
    {
        ActiveBallEffectIndex = 0;
        ActiveBallSoundIndex = 0;
        Level_Name = SceneManager.GetActiveScene().name;

        Bucket_st_value = .5f;
        Bucket_Step_value =.25f / HdfBallSys;
        
        LevelSlider.maxValue = HdfBallSys;
        KlnBallSys_Text.text = MvctBallSys.ToString();
        
    }

    public void BallEntered()
    {
        GrnBallSys++;
        LevelSlider.value = GrnBallSys;

        Bucket_st_value -= Bucket_Step_value;

        BucketTransparent.material.SetTextureScale("_MainTex",new Vector2(1f,Bucket_st_value));

        BallSounds[ActiveBallSoundIndex].Play();
        ActiveBallSoundIndex++;

        if (ActiveBallSoundIndex == BallSounds.Length - 1)
        {
            ActiveBallSoundIndex= 0;
        }
        if (GrnBallSys==HdfBallSys)
        {
            Time.timeScale = 0;
            Other_Sound[1].Play();
            PlayerPrefs.SetInt("Level",SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Star",PlayerPrefs.GetInt("Star")+15);
            StarNumber.text = PlayerPrefs.GetInt("Star").ToString();
            Win_Level_Number.text ="LEVEL : "+ Level_Name;
            Panels[1].SetActive(true);
        }

        int Number = 0;
        foreach (var item in Balls)
        {
            if (item.activeInHierarchy)
            {
                Number++;
            }
        }

        if (Number==0)
        {
            if (MvctBallSys == 0 && GrnBallSys != HdfBallSys)
            {
                Lost();
            }
            if ((MvctBallSys + GrnBallSys) < HdfBallSys)
            {
                Lost();
            }
        }

        
    }
    public void BallNotEntered()
    {
        int Number = 0;
        foreach (var item in Balls)
        {
            if (item.activeInHierarchy)
            {
                Number++;
            }
        }

        if (Number == 0)
        {
            if (MvctBallSys == 0 && GrnBallSys != HdfBallSys)
            {
                Lost();
            }
            if ((MvctBallSys + GrnBallSys) < HdfBallSys)
            {
                Lost();
            }
        }

        if (MvctBallSys==0)
        {
           Lost();
        }

        if ((MvctBallSys+GrnBallSys)<HdfBallSys)
        {
            Lost();
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

    public void ParcEffect(Vector3 Pos, Color _color)
    {
        BallEffects[ActiveBallEffectIndex].transform.position = Pos;

        var main = BallEffects[ActiveBallEffectIndex].main;
        main.startColor = _color;
        BallEffects[ActiveBallEffectIndex].gameObject.SetActive(true);
        ActiveBallEffectIndex++;

        if (ActiveBallEffectIndex == BallEffects.Length - 1)
        {
            ActiveBallEffectIndex = 0;
        }
    }

    void Lost()
    {
        Time.timeScale = 0;
        Other_Sound[0].Play();
        Lost_Level_Number.text = "LEVEL : " + Level_Name;
        Panels[2].SetActive(true);
    }

    public void Ball_Throw()
    {
        if (Time.timeScale!=0)
        {
            MvctBallSys--;
            KlnBallSys_Text.text = MvctBallSys.ToString();
            _Ball_Animator.Play("Ball_Atr");
            _Ball_Throw_Effect.Play();
            Other_Sound[2].Play();
            Balls[ActiveBallIndex].transform
                .SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
            Balls[ActiveBallIndex].SetActive(true);
            Balls[ActiveBallIndex].GetComponent<Rigidbody>().AddForce(
                Balls[ActiveBallIndex].transform.TransformDirection(90, 90, 0) * BallForce, ForceMode.Force);

            if (Balls.Length - 1 == ActiveBallIndex)
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

