using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScrollBackground : MonoBehaviour {
    public float scrollSpeed;
    private Vector2 savedOffset;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        savedOffset = rend.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        if (GameManager.Instance.PlayerCurrentGameState == GameManager.GameStates.PLAYING || GameManager.Instance.PlayerCurrentGameState == GameManager.GameStates.PLAYING_WITH_STYLE)
        {
            float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
            Vector2 offset = new Vector2(x, savedOffset.x);
            rend.sharedMaterial.SetTextureOffset("_MainTex", offset);
        }
    }

    void OnDisable()
    {
        rend.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
