using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.SceneManagement;


public class VideoScriptLancement : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;

    void Update()
    {
        if (videoPlayer.isPaused)
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
