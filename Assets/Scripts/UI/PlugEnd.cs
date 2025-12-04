using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugEnd : MonoBehaviour
{
    public LineRenderer line;
    private bool placed = false;
    public Sprite unpluggedSprite;
    public Sprite pluggedSprite;
    public SpriteRenderer sRenderer;
    

    // Update is called once per frame
    void Update()
    {
        if (placed) return;

        Vector3 linePoint = line.GetPosition(line.positionCount-3);
        Vector2 lookDir =  transform.position - linePoint;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Placed()
    {
        placed = true;
        sRenderer.sprite = pluggedSprite;
        transform.rotation = Quaternion.identity;
        sRenderer.sortingOrder = 1;
    }

    public void Unplaced()
    {
        placed = false;
        sRenderer.sprite = unpluggedSprite;
        sRenderer.sortingOrder = 3;
    }
}
