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
    [SerializeField]
    private SeatGenerator seatGenerator;

    [SerializeField]
    private float singlePersonSoundIncrement;
    [SerializeField]
    private float pairSoundIncrement;
    [SerializeField]
    private float tripleSoundIncrement;
    [SerializeField]
    private float lShapeSoundIncrement;

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
        textHolder.text = "Sound Level : " + Mathf.RoundToInt(soundSlider.value * 100.0f) + " %";
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMatches();
        classRoomClip.volume = soundSlider.value;
        textHolder.text = "Sound Level : " + Mathf.RoundToInt(soundSlider.value * 100.0f) + " %";
        //print(soundSlider.value);
        currentNoiseLevel = soundSlider.value * 100;
    }

    private void CheckForMatches()
    {
        int countOfPairs = 0;
        int countOfTriplets = 0;
        int countOfLShapes = 0;
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
                soundSlider.value += pairSoundIncrement;
                //textHolder.text = "Sound Level : " + Mathf.RoundToInt(soundSlider.value * 100.0f) + " %";
            }
            //three people in one row
            else if(i >= 2 // to prevent negative indexing
                    && (seats[i].studentType != StudentType.NO_STUDENT && seats[i - 1].studentType != StudentType.NO_STUDENT && seats[i - 2].studentType != StudentType.NO_STUDENT) // to check for non empty seats
                    && (seats[i].studentType == seats[i - 1].studentType && seats[i].studentType == seats[i - 2].studentType) // matching of student type
                    && (seats[i].yCoordinate == seats[i - 1].yCoordinate && seats[i].yCoordinate == seats[i - 2].yCoordinate) // same row
                    && (seats[i].matchForThisTile != MatchType.TRIPLE)) // triple students of same type condition
            {
                countOfTriplets++;
                seats[i].matchForThisTile = MatchType.TRIPLE;
                seats[i - 1].matchForThisTile = MatchType.TRIPLE;
                seats[i - 2].matchForThisTile = MatchType.TRIPLE;
                soundSlider.value += tripleSoundIncrement;
                //textHolder.text = "Sound Level : " + Mathf.RoundToInt(soundSlider.value * 100.0f) + " %";
            }
            // L shape
            if((i + 3 < seats.Count)
                && (seats[i].studentType != StudentType.NO_STUDENT)
                && (seats[i].studentType == seats[i - 1].studentType) &&  seats[i].studentType == seats[i + 3].studentType
                && ( seats[i].matchForThisTile == MatchType.PAIR || seats[i].matchForThisTile == MatchType.NO_MATCH))
            {
                //print("took an L");
                countOfLShapes++;
                seats[i].matchForThisTile = MatchType.L_SHAPE;
                seats[i - 1].matchForThisTile = MatchType.L_SHAPE;
                seats[i + 3].matchForThisTile = MatchType.L_SHAPE;
                soundSlider.value += lShapeSoundIncrement;

            }
            else if ((i + 3 < seats.Count)
                && (seats[i].studentType != StudentType.NO_STUDENT)
                && (seats[i].studentType == seats[i - 1].studentType) &&  seats[i - 1].studentType == seats[i + 2].studentType
                && (seats[i].matchForThisTile == MatchType.PAIR || seats[i].matchForThisTile == MatchType.NO_MATCH))
            {
                //print("took an L");
                countOfLShapes++;
                seats[i].matchForThisTile = MatchType.L_SHAPE;
                seats[i - 1].matchForThisTile = MatchType.L_SHAPE;
                seats[i + 2].matchForThisTile = MatchType.L_SHAPE;
                soundSlider.value += lShapeSoundIncrement;

            }

        }
        //print(seatGenerator.GetRows());
        //print("number of pairs : " + countOfPairs + " ,");
        //print("number of triplets : " + countOfTriplets + ", ");
        //print("number of L shapes : " + countOfLShapes);
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
        soundSlider.value += singlePersonSoundIncrement;
        textHolder.text = "Sound Level : " + Mathf.RoundToInt(soundSlider.value * 100.0f) + " %";
    }



}
