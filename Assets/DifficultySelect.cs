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
            Destroy(gameObject,3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetDifficulty("StreamingAssets/song_hackathon_easy.xml");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetDifficulty("StreamingAssets/song_hackathon_medium.xml");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetDifficulty("StreamingAssets/song_hackathon_hard.xml");
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
