using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    enum FadeState
    {
        In,
        Out,
        None
    }

    public Image fadeImage;

    private float timeToFade;
    private Color fadeColor;

    public Color defaultFadeColor = Color.black;
    private float timeFading;
    public static SceneLoadManager Instance;
    private FadeState state = FadeState.None;
    private string sceneNameToLoad;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(state != FadeState.None)
        {
            timeFading += Time.deltaTime;
            //If finished fading
            if(timeFading >= timeToFade)
            {
                //Reset timer
                timeFading = 0;
                if(state == FadeState.Out)
                {
                    //Load and blackout
                    SceneManager.LoadScene(sceneNameToLoad);
                    fadeImage.color = fadeColor;
                } else if (state == FadeState.In)
                {
                    //make colour clear
                    fadeImage.color = Color.clear;
                }
                state = FadeState.None;
            } else //If not finished fading
            {
                float alpha = 0;
                if(state == FadeState.Out)
                {
                    alpha = Mathf.Lerp(0, 1, timeFading / timeToFade);
                } else if (state == FadeState.In)
                {
                    alpha = Mathf.Lerp(1, 0, timeFading / timeToFade);
                }
                fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        state = FadeState.In;
    }

    //Function only requires name, can override timeToFade
    public void LoadSceneWithFade(string name, float newTimeToFade = 0.5f)
    {
        state = FadeState.Out;
        fadeColor = defaultFadeColor;
        sceneNameToLoad = name;
        timeToFade = newTimeToFade;
    }

    //Overload to override color
    public void LoadSceneWithFade(string name, Color newFadeColor, float newTimeToFade=0.5f)
    {
        state = FadeState.Out;
        fadeColor = newFadeColor;
        sceneNameToLoad = name;
        timeToFade = newTimeToFade;
    }
}
