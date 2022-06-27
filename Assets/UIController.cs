using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI level;

    [SerializeField] TextMeshProUGUI firstNum;
    [SerializeField] TextMeshProUGUI calculation;
    [SerializeField] TextMeshProUGUI lastNum;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Slider slider;

    [SerializeField] GameObject gameOver;

    public void UpdateCalculation(int first, string calculation, int last)
    {
        //firstNum.text = first.ToString();
        //this.calculation.text = calculation;
        //lastNum.text = last.ToString();
        int resultCal = 0;
        int[] result = new int[2];
        if(calculation == "+")
        {
            resultCal = first + last;
            //bool both = result[random] == result[1 - random];

            //GameController.Instance.UpdateAnswer(random, both);
        }
        else
        {
            resultCal = first - last;

            //bool both = result[random] == result[1 - random];
            //GameController.Instance.UpdateAnswer(random, both);
        }

        int randomHide = Random.Range(0, 3);
        if(randomHide == 0)
        {
            //hide first
            firstNum.text = "__ " + calculation + " " + last + " = " + resultCal;
            int random = Random.Range(0, 2);
            result[random] = first;
            result[1 - random] = Random.Range(1, 20);

            bool both = result[random] == result[1 - random];
            GameController.Instance.UpdateAnswer(random, both);
        }
        else if (randomHide == 1)
        {
            //hide last
            firstNum.text = first + " " + calculation + " __" + " = " + resultCal;
            int random = Random.Range(0, 2);
            result[random] = last;
            result[1 - random] = Random.Range(1, 20);

            bool both = result[random] == result[1 - random];
            GameController.Instance.UpdateAnswer(random, both);
        }
        else 
        {
            //hide result
            firstNum.text = first + " " + calculation + " " + last + " = __";
            int random = Random.Range(0, 2);
            result[random] = resultCal;
            result[1 - random] = Random.Range(1, 20);

            bool both = result[random] == result[1 - random];
            GameController.Instance.UpdateAnswer(random, both);
        }

        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = result[0].ToString();
        rightButton.GetComponentInChildren<TextMeshProUGUI>().text = result[1].ToString();
    }

    public void SetSlider(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void UpdateSlider(float value)
    {
        slider.value = value;
    }

    public void UpdateHighscore(int value)
    {
        highScore.text = value.ToString();
    }

    public void UpdateLevel(int value)
    {
        level.text = value.ToString();
    }

    public void ShowGameOver(bool isShow)
    {
        gameOver.SetActive(isShow);
    }
}
