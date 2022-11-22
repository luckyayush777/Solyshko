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
    [SerializeField]
    private TextMeshProUGUI textHolder;
    [SerializeField]
    private AudioSource classRoomClip;

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
        textHolder.text = "Sound Level : " + soundSlider.value * 100.0f + " %";
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMatches();
        classRoomClip.volume = soundSlider.value;
    }

    private void CheckForMatches()
    {
        int countOfPairs = 0;
        int countOfTriplets = 0;
        for(int i = 1; i < seats.Count; i++)
        {
            //simplest case, a pair
            if (seats[i].studentType != StudentType.NO_STUDENT && seats[i - 1].studentType != StudentType.NO_STUDENT 
                && seats[i].studentType == seats[i - 1].studentType
                && seats[i].yCoordinate == seats[i - 1].yCoordinate 
                && ( seats[i].matchForThisTile == MatchType.NO_MATCH && seats[i - 1].matchForThisTile == MatchType.NO_MATCH ) )
            {
                seats[i].matchForThisTile = MatchType.PAIR;
                seats[i - 1].matchForThisTile = MatchType.PAIR;
                countOfPairs++;
                soundSlider.value += 0.07f;
                textHolder.text = "Sound Level : " + Mathf.RoundToInt(soundSlider.value * 100.0f) + " %";
                Debug.Log(soundSlider.value);
            }
            //three people in one row
            else if(i >= 2 // to prevent negative indexing
                    && (seats[i].studentType != StudentType.NO_STUDENT && seats[i - 1].studentType != StudentType.NO_STUDENT && seats[i - 2].studentType != StudentType.NO_STUDENT) // to check for non empty seats
                    && (seats[i].studentType == seats[i - 1].studentType && seats[i].studentType == seats[i - 2].studentType) // matching of student type
                    && (seats[i].yCoordinate == seats[i - 1].yCoordinate && seats[i].yCoordinate == seats[i - 2].yCoordinate) // same row
                    && (seats[i].matchForThisTile != MatchType.TRIPLE)) // triple students of same type condition
            {
                print("triplet condition");
                countOfTriplets++;
                seats[i].matchForThisTile = MatchType.TRIPLE;
                seats[i - 1].matchForThisTile = MatchType.TRIPLE;
                seats[i - 2].matchForThisTile = MatchType.TRIPLE;
                soundSlider.value += 0.1f;
                textHolder.text = "Sound Level : " + Mathf.RoundToInt(soundSlider.value * 100.0f) + " %";
            }

        }
        print("number of pairs : " + countOfPairs + " ,");
        print("number of triplets : " + countOfTriplets);

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
        textHolder.text = "Sound Level : " + Mathf.RoundToInt(soundSlider.value * 100.0f) + " %";
    }



}
