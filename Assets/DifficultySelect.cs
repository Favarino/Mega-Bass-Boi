using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelect : MonoBehaviour {

    bool selected = false;

    private void Start()
    {
        DifficultyBottle.onHit += SetDifficulty;
    }

    private void Update()
    {
        if(GameManager.Instance.PlayerCurrentGameState != GameManager.GameStates.WAITING_TO_START)
        {
            Destroy(gameObject);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            SetDifficulty("StreamingAssets/song_hackathon_medium.xml");
        }
    }


    void SetDifficulty(string path)
    {
        print("setting Difficulty");
        if (!selected&&GameManager.Instance.PlayerCurrentGameState == GameManager.GameStates.WAITING_TO_START)
        {
            GameManager.Instance.SetPath(path);
            selected = true;

            Destroy(gameObject, 3f);
        }
    }
}
