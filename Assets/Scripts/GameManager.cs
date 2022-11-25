using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI allowedNoiseLevelText;

    public static float allowedNoiseLevel = 50;
    // really need to remove these static public floats
    public static bool timeElapsed = false;
    public static bool seatsFilled = false;
    public static bool gameLossCondition = false;
    public static bool gameVictoryCondition = false;

    private void Start()
    {
        allowedNoiseLevelText.text = "The Allowed Noise is " + allowedNoiseLevel;
    }

    private void Update()
    {
        if (NoiseManager.currentNoiseLevel > allowedNoiseLevel || timeElapsed)
        {
            gameLossCondition = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (seatsFilled && NoiseManager.currentNoiseLevel <= allowedNoiseLevel && !timeElapsed)
        {
            print("victory condition");
            gameVictoryCondition = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        
        }
    }

    
}
