using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoint : MonoBehaviour
{
    public Transform pointTarget;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction =  pointTarget.position - transform.position;
        float degrees = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        gameObject.transform.eulerAngles = new Vector3(0, 0, degrees);
    }

    public void ChangeTarget(Transform newTrans)
    {
        pointTarget=newTrans;
    }
}
