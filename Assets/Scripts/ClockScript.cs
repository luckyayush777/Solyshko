using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClockScript : MonoBehaviour
{
    [SerializeField]
    private RectTransform handTransform;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private float gameDuration;
    private AudioSource audioPlayer;
    [SerializeField]
    private AudioClip warningSound;
    private bool warningSoundGiven = false;
    

    public static float timeSinceGameStart;

    private void Start()
    {
        timeSinceGameStart = 0;
        audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timeSinceGameStart += Time.deltaTime;
        string seconds = (timeSinceGameStart % 60).ToString("00");
        handTransform.eulerAngles = new Vector3(0, 0, Time.realtimeSinceStartup * -90.0f);
        if (warningSoundGiven == false && gameDuration - timeSinceGameStart <= 5.0f)
        {
            audioPlayer.clip = warningSound;
            audioPlayer.Play();
            warningSoundGiven = true;
        }
            
        if (timeSinceGameStart <= gameDuration)
            timeText.text = seconds;
        else
        {
            GameManager.timeElapsed = true;
            handTransform.eulerAngles = new Vector3(0, 0, Time.realtimeSinceStartup * 0.0f);

        }
    }
}
