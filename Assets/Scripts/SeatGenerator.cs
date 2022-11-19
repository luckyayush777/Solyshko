using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StudentType
{
    NO_STUDENT = 0,
    MALE_STUDENT = 1,
    FEMALE_STUDENT = 2

};

public class SeatGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int noOfRows = 0;
    private float seatGap = 0.35f;
    [SerializeField]
    private GameObject seatPrefab;
    [SerializeField]
    private Transform firstSeatPos;
    [SerializeField]
    private Sprite maleStudentSprite;
    //TODO : gloBAL state variable
    public static bool seatOnHandState = false;
    //TODO : gloBAL state variable
    public static StudentType typeAtHand = StudentType.NO_STUDENT;

    private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField]
    private Texture2D cursorTexture;
    [SerializeField]
    private Texture2D cursorTextureFemale;

    

    void Start()
    {
        InitiateSeats();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void InitiateSeats()
    {
        Vector3 initPos = firstSeatPos.position;
        Vector3 currentInitPos = initPos;
        for(int i = 0; i < noOfRows; i++)
        {
            for(int j = 0; j < noOfRows; j++)
            {
                GameObject currentSeat = Instantiate(seatPrefab, currentInitPos, Quaternion.identity, transform);
                currentInitPos.x += seatGap;
                currentSeat.GetComponent<SeatBehaviour>().xCoordinate = j + 1;
                currentSeat.GetComponent<SeatBehaviour>().yCoordinate = i + 1;
            }
            currentInitPos.y += seatGap;
            currentInitPos.x = initPos.x;

        }
    }

    // In general the on click behaviours need to be seperated to a different script

    //change name of the function
    public void ChangeCursorToMaleStudentSprite()
    {
        seatOnHandState = true;
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
        typeAtHand = StudentType.MALE_STUDENT;
    }

    public void ChangeCursorToFemaleStudentSprite()
    {
        seatOnHandState = true;
        Cursor.SetCursor(cursorTextureFemale, Vector2.zero, cursorMode);
        typeAtHand = StudentType.FEMALE_STUDENT;
    }

    public static void ChangeCursorToDefault()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
