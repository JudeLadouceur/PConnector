using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSpinny : MonoBehaviour
{
    public float degPerSec;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, degPerSec * Time.deltaTime);
    }
}
