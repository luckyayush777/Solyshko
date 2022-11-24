using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SeatBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Sprite maleStudentSprite;
    [SerializeField]
    private Sprite femaleStudentSprite;
    private SpriteRenderer childRenderer;
    public StudentType studentType = StudentType.NO_STUDENT;
    public int xCoordinate;
    public int yCoordinate;
    public int index;
    public MatchType matchForThisTile = MatchType.NO_MATCH;
    public delegate void OnSeatingStudent();
    public static event OnSeatingStudent OnClickingSeat;
    private AudioSource sittingClip;
    void Start()
    {
        childRenderer = GetComponentInChildren<SpriteRenderer>();
        sittingClip = GetComponent<AudioSource>();
        if(sittingClip == null)
        {
            print("couldnt attach audio source to script");
        }
        if (childRenderer == null)
            Debug.Log("Couldnt find sprite renderer in child component");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (SeatGenerator.seatOnHandState == true)
        {
            if (SeatGenerator.typeAtHand == StudentType.MALE_STUDENT)
            {
                studentType = StudentType.MALE_STUDENT;
                childRenderer.sprite = maleStudentSprite;
                sittingClip.Play();
                OnClickingSeat?.Invoke();
            }
            else if (SeatGenerator.typeAtHand == StudentType.FEMALE_STUDENT)
            {
                studentType = StudentType.FEMALE_STUDENT;
                childRenderer.sprite = femaleStudentSprite;
                sittingClip.Play();

                OnClickingSeat?.Invoke();

            }
            SeatGenerator.seatOnHandState = false;
            SeatGenerator.ChangeCursorToDefault();
        }
    }

    private void OnSeatClick()
    {

    }
}
