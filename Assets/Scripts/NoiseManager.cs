using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseManager : MonoBehaviour
{
    private List<SeatBehaviour> seats = new List<SeatBehaviour>();
    [Range(0, 1)]
    public static float currentNoiseLevel = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(DelayFindSeats(0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentNoiseLevel);
    }
    
    private IEnumerator DelayFindSeats(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        foreach (GameObject seat in GameObject.FindGameObjectsWithTag("seat"))
        {
            seats.Add(seat.GetComponent<SeatBehaviour>());
            Debug.Log("added seat");
        }
    }

}
