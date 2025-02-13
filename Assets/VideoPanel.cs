using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPanel : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private void Start()
    {
        AudioManager.instance.MuteEverything(true);
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnded;
    }

    void OnVideoEnded(VideoPlayer vp)
    {
        gameObject.SetActive(false);
        AudioManager.instance.MuteEverything(false);
    }
}
