using UnityEngine;
using TMPro;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI TMPUGUI;

    private float _timer = 0f;
    private int _minutes = 0;
    private int _seconds = 0;

    void Start()
    {
        StartCoroutine(IncrementTimer());
    }

    IEnumerator IncrementTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);  // wait for 1 second

            _timer += 1f;  // increment the timer by 1 second

            if (_seconds >= 59f)  // if a minute has passed, reset the seconds and increment the minutes
            {
                _minutes++;
                _seconds = 0;
            }
            else
            {
                _seconds++;
            }

            TMPUGUI.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);  // format the timer as "mm:ss"
        }
    }
}
