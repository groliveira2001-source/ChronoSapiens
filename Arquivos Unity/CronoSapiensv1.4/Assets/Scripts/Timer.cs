using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TMP_Text uiText;

   

    public int Duration;

    private int remaingDuration;

    // Start is called before the first frame update
    void Start()
    {
        Being(Duration);
    }

    private void Being(int Second)
    {
        remaingDuration = Second;
        StartCoroutine(UpdateTimer());

    }

    private IEnumerator UpdateTimer()
    {
        while(remaingDuration >= 0)
        {
            uiText.text = $"{remaingDuration / 60:00} : {remaingDuration % 60:00}"; 
            uiFill.fillAmount = Mathf.InverseLerp(0,Duration, remaingDuration);
            remaingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        print("Time's Up");

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.ShowGameOver();
        }

    }

   
}
