using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    public VideoPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player.loopPointReached += LoadScene;
    }

    public void LoadScene(VideoPlayer vp)
    {
        SceneManager.instance.GoToNextScene();
    }
}
