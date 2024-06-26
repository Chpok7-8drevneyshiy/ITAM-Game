using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Skip : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Game");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("Game");
    }
}
