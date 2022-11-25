using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndgameBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI endgameTextHolder;
    private AudioSource endgameAudio;
    [SerializeField]
    private AudioClip lossClip;
    [SerializeField]
    private AudioClip victoryClip;
    private bool endgameOccured = false;
    void Start()
    {
        endgameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if( GameManager.gameVictoryCondition)
        {
            endgameTextHolder.text = "There hasnt been a quieter classroom, You won!";
            if(!endgameOccured)
            {
                endgameAudio.clip = victoryClip;
                endgameAudio.Play();
                endgameOccured = true;
            }
        }
        else if(GameManager.gameLossCondition)
        {
            endgameTextHolder.text = "The teacher ran away, You lose...";
            if (!endgameOccured)
            {
                endgameAudio.clip = lossClip;
                endgameAudio.Play();
                endgameOccured = true;
            }
        }
    }
}
