using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoCreditsLancement : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string VideoName;

    InputAction touche_echap;
    private void Start()
    {
        touche_echap = InputSystem.actions.FindAction("Echap");

        string videoPath = Application.streamingAssetsPath + "/Videos/" + VideoName;
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = videoPath;

        videoPlayer.Prepare();
        videoPlayer.Play();

    }
    void Update()
    {
        if (videoPlayer.isPaused || touche_echap.WasPerformedThisFrame())
        {
            if (SceneManager.GetActiveScene().name == "Credits")
            {
                SceneManager.LoadScene("VideoLancement");
            }
            else
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }
}
