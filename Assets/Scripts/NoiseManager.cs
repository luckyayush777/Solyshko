using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NoiseManager : MonoBehaviour
{
    private List<SeatBehaviour> seats = new List<SeatBehaviour>();
    [Range(0, 1)]
    public static float currentNoiseLevel = 0;
    [SerializeField]
    private Slider soundSlider;

    private void OnEnable()
    {
        SeatBehaviour.OnClickingSeat += ChangeSound;
    }

    private void OnDisable()
    {
        SeatBehaviour.OnClickingSeat -= ChangeSound;
    }
    private void Awake()
    {
        StartCoroutine(DelayFindSeats(0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMatches();
    }

    private void CheckForMatches()
    {
        int countOfPairs = 0;
        //simplest case, a couple
        for(int i = 1; i < seats.Count; i++)
        {
            if (seats[i].studentType != StudentType.NO_STUDENT && seats[i].studentType == seats[i -1].studentType)
                countOfPairs++;
        }
        print(countOfPairs);
    }
    
    private IEnumerator DelayFindSeats(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        foreach (GameObject seat in GameObject.FindGameObjectsWithTag("seat"))
        {
            seats.Add(seat.GetComponent<SeatBehaviour>());
        }
    }

    private void ChangeSound()
    {
        soundSlider.value += 0.05f;
    }



}
