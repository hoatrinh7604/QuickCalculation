using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private int highScore = 0;
    private int currentLevel = 0;

    [SerializeField] float maxValueTime = 5;

    private float time;
    private float timeSystem;
    private string[] cal = { "summation", "subtraction"};
    private int currentCal;
    private UIController uiController;

    public int rightAnswer;
    private bool bothAnswerRight;

    private enum Cal
    {
        summation = 0,
        subtraction = 1
    }

    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();

        Reset();
        StartNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        timeSystem += Time.deltaTime;
        time -= Time.deltaTime;

        UpdateSlider(time);

        if(time < 0)
        {
            // GameOver
            GameOver();
        }

        if(timeSystem > 30)
        {
            maxValueTime--;
            if (maxValueTime < 2) maxValueTime = 2;
            timeSystem = 0;
        }
    }

    public void Reset()
    {
        time = maxValueTime;
        timeSystem = 0;
        currentLevel = 0;
        highScore = PlayerPrefs.GetInt("highscore");
        uiController.SetSlider(maxValueTime);
        uiController.UpdateSlider(maxValueTime);
        uiController.ShowGameOver(false);
        bothAnswerRight = false;
    }

    public void UpdateLevel(int level)
    {
        GetComponent<UIController>().UpdateLevel(level);
    }

    public void StartNextLevel()
    {
        currentLevel++;
        UpdateLevel(currentLevel);

        if(highScore < currentLevel)
        {
            highScore = currentLevel;
            PlayerPrefs.SetInt("highscore", highScore);
        }

        int firstNum = Random.Range(1, 20);
        int lastNum = Random.Range(1, 20);
        SetCal(firstNum, lastNum);
        UpdateLevelInfo(firstNum, lastNum);
        time = maxValueTime;
        bothAnswerRight = false;
    }

    public void SetCal(int first, int last)
    {
        if (first < last)
            currentCal = (int)(Cal.summation);
        else
        {
            int random = Random.Range(0, 1);
            if(random==0) currentCal = (int)(Cal.summation);
            currentCal = (int)(Cal.subtraction);
        }
    }

    public void UpdateLevelInfo(int firstNum, int lastnum)
    {
        string cal = "";
        if (currentCal == 0) cal = "+";
        else cal = "-";

        uiController.UpdateCalculation(firstNum, cal, lastnum);
        uiController.UpdateLevel(currentLevel);
        uiController.UpdateHighscore(highScore);
    }

    public void UpdateAnswer(int value, bool both = false)
    {
        rightAnswer = value;
        bothAnswerRight = both;
    }

    public void CheckAnswer(int value)
    {
        if(rightAnswer == value || bothAnswerRight)
        {
            StartNextLevel();
        }
        else
        {
            //Game over
            GameOver();
        }
    }

    public void UpdateSlider(float value)
    {
        uiController.UpdateSlider(value);
    }

    public void GameOver()
    {
        uiController.ShowGameOver(true);
    }
}
