using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeObject : MonoBehaviour
{
    public GameObject endPos;
    public GameObject startPos;
    public GameObject lineRoot;
    private bool isMoving = true;
    private LineManager lm;
    public PlugEnd plug;

    // Start is called before the first frame update
    void Start()
    {
        lineRoot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPos.transform.position = mousePos + new Vector3(0,0,9);
    }

    public void StartLine(GameObject startNotch)
    {
        lineRoot.SetActive(true);
        startPos.transform.position = startNotch.transform.position - new Vector3(0, 0, 1);
        isMoving = true;
        plug.Unplaced();
    }

    public void LockLine(GameObject endNotch)
    {
        plug.Placed();
        isMoving = false;
        endPos.transform.position = endNotch.transform.position - new Vector3(0, 0, 1);
        
    }

    public void EliminateLine()
    {
        lineRoot.SetActive(false);
        isMoving = false;
    }
}
