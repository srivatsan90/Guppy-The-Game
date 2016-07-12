using UnityEngine;
using UnityEngine.UI;

public class GuppyGameManager : MonoBehaviour
{
    private int Changing_Number, Random_Number, Changing_Pattern, Random_Pattern, score, HighScore, HighScoreScore;
    private float Initial_Time, Time_Elapsed = 3f;
    public Text Changing_color_Display, GameOver_Text, Score_Text, HighScore_Text, Menu_HighScore;
    public int correctClicks, NewScore;
    public float level_Timer, max_LevelTime, speed;
    public Animator Fish_Catch_Animator;
    public GameObject GameOver_panel, start_Screen;
    public GameObject Fish1, Fish2, Fish3, Fish4, Fish5, Fish6;
    private bool gameStart;
    public GameObject[] Images, Color_Patterns;
    public Image Timer_Image;
    public AudioClip BG, Menu_Button_Click,In_Game_Button_Click,Caught_Fish;
    private AudioSource Sorce;

    private void Start()
    {
        GetRandomColor();
        GetRandomPattern();
        HighScoreScore = PlayerPrefs.GetInt("HighScore", 0);
        Menu_HighScore.text = "HighScore :" + HighScoreScore;
    }

    private void Awake()
    {
        Sorce = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (gameStart)
        {
            Debug.Log(HighScore);
            SwitchColor();
            SwitchPattern();
            FishCathced();
            level_Timer += Time.deltaTime;
            if (correctClicks == 1)
            {
                Fish_Catch_Animator.SetBool("Catch", false);
            }
            GameOver();
            Score_Text.text = "Score:" + score;
            HighScore_Text.text = "HighScore :" + HighScoreScore;
            if (score > HighScoreScore)
            {
                HighScoreScore = score;
                HighScore_Text.text = "HighScore :" + HighScoreScore;
            }
        }
        else
        {
            Images[3].SetActive(false);
            Images[0].SetActive(false);
            Images[1].SetActive(false);
            Images[2].SetActive(false);
        }
        FishMovement();
        //PlayerPrefs.DeleteAll();
        Timer_Image.fillAmount -= level_Timer / 105000;
    }

    private void GetRandomColor()
    {
        Changing_Number = Random.Range(1, 5);

        while (Random_Number == Changing_Number)
        {
            Changing_Number = Random.Range(1, 5);
        }

        Random_Number = Changing_Number;
    }

    private void SwitchColor()
    {
        switch (Changing_Number)
        {
            case 1:

                Images[0].SetActive(true);
                Images[1].SetActive(false);
                Images[2].SetActive(false);
                Images[3].SetActive(false);

                break;

            case 2:

                Images[0].SetActive(false); Images[1].SetActive(false); Images[2].SetActive(true); Images[3].SetActive(false);

                break;

            case 3:

                Images[0].SetActive(false); Images[1].SetActive(true); Images[2].SetActive(false); Images[3].SetActive(false);

                break;

            case 4:

                Images[3].SetActive(true);
                Images[0].SetActive(false);
                Images[1].SetActive(false);
                Images[2].SetActive(false);

                Initial_Time += Time.deltaTime;

                if (Initial_Time > Time_Elapsed)
                {
                    GetRandomColor();
                    Initial_Time = 0;
                }
                break;
        }
    }

    public void OnRedButton()
    {
        if (Changing_Number == 1)
        {
            correctClicks += 1;
        }
        else
        {
            correctClicks = 0;
        }
        GetRandomColor();
        GetRandomPattern();
        Sorce.PlayOneShot(In_Game_Button_Click);
    }

    public void OnGreenButton()
    {
        if (Changing_Number == 2)
        {
            correctClicks += 1;
        }
        else
        {
            correctClicks = 0;
        }
        GetRandomColor();
        GetRandomPattern();
        Sorce.PlayOneShot(In_Game_Button_Click);
    }

    public void OnBlueButton()
    {
        if (Changing_Number == 3)
        {
            correctClicks += 1;
        }
        else
        {
            correctClicks = 0; ;
        }
        GetRandomColor();
        GetRandomPattern();
        Sorce.PlayOneShot(In_Game_Button_Click);
    }

    public void OnYellowButton()
    {
        if (Changing_Number == 4)
        {
            correctClicks += 1;
        }
        else
        {
            correctClicks = 0; ;
        }
        GetRandomColor();
    }

    private void FishCathced()
    {
        if (correctClicks == 5)
        {
            Sorce.PlayOneShot(Caught_Fish);
            correctClicks = 0;
            Debug.Log("Animation Played well");
            Fish_Catch_Animator.SetBool("Catch", true);
            score += NewScore;
            if (score >= HighScore)
            {
                HighScore = score;
                PlayerPrefs.SetInt("HighScore", HighScore);
            }
        }
    }

    public void OnPlay()
    {
        start_Screen.SetActive(false);
        gameStart = true;
        Sorce.PlayOneShot(Menu_Button_Click);
    }

    private void GameOver()
    {
        if (level_Timer > max_LevelTime)
        {
            GameOver_panel.SetActive(true);
            GameOver_Text.text = "Guppies Caught: " + score;
            gameStart = false;
        }
    }

    public void Restart()
    {
        score = 0;
        level_Timer = 0f;
        GameOver_panel.SetActive(false);
        gameStart = true;
        Timer_Image.fillAmount = 1;
        Sorce.PlayOneShot(Menu_Button_Click);
    }

    public void FishMovement()
    {
    }

    private void GetRandomPattern()
    {
        Changing_Pattern = Random.Range(1, 5);

        while (Random_Pattern == Changing_Pattern)
        {
            Changing_Pattern = Random.Range(1, 5);
        }

        Random_Pattern = Changing_Pattern;
    }

    private void SwitchPattern()
    {
        switch (Changing_Pattern)
        {
            case 1:

                Color_Patterns[0].SetActive(true); Color_Patterns[1].SetActive(false); Color_Patterns[2].SetActive(false); Color_Patterns[3].SetActive(false);

                break;

            case 2:
                Color_Patterns[0].SetActive(false); Color_Patterns[1].SetActive(true); Color_Patterns[2].SetActive(false); Color_Patterns[3].SetActive(false);
                break;

            case 3:
                Color_Patterns[0].SetActive(false); Color_Patterns[1].SetActive(false); Color_Patterns[2].SetActive(true); Color_Patterns[3].SetActive(false);
                break;

            case 4:
                Color_Patterns[0].SetActive(false); Color_Patterns[1].SetActive(false); Color_Patterns[2].SetActive(false); Color_Patterns[3].SetActive(true);
                break;
        }
    }

    public void Exit()
    {
        Application.Quit();
        Sorce.PlayOneShot(Menu_Button_Click);
    }
}