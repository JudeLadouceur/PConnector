using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScroll : MonoBehaviour
{

    public float startPauseTime;
    public float timeToScroll;
    public float endPauseTime;
    public GameObject credits;
    public float endPosOffest;
    private Vector3 endPosition;
    private float timer = 0f;
    private bool startPaused = true;
    private bool endPaused = false;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Invoke("GetCreditsHeight", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(startPaused || endPaused)
        {
            timer += Time.deltaTime;
            if((startPaused && timer >= startPauseTime))
            {
                startPaused = false;
                timer = 0f;
            }
            if(endPaused &&  timer >= endPauseTime)
            {
                Debug.Log("Credit Scroll Done");
            }
        }
        else
        {
            timer += Time.deltaTime;
            Vector3 currentPos = Vector3.Lerp(startPos, endPosition, timer / timeToScroll);
            credits.transform.position = currentPos;
            if (timer >= timeToScroll)
            {
                endPaused = true;
                timer = 0f;
            }
        }
    }

    private void GetCreditsHeight()
    {
        endPosition = new Vector3(credits.transform.position.x, credits.GetComponent<RectTransform>().rect.height+endPosOffest, 0);
    }
}
