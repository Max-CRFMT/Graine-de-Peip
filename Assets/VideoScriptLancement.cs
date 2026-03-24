using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class VideoScriptLancement : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;
    InputAction touche_echap;
    private void Start()
    {
        touche_echap = InputSystem.actions.FindAction("Echap");
    }
    void Update()
    {
        if (videoPlayer.isPaused || touche_echap.WasPerformedThisFrame())
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
