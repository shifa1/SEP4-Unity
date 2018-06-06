using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampRectTransform : MonoBehaviour {

    public float padding = 10.0f;
    public float elementSize = 128.0f;
    public float viewSize = 250.0f;

    private RectTransform rt;
    private int amountElements;
    private float contentSize;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Clamp our rect transform, returns children
        amountElements = rt.childCount;
        contentSize = (amountElements * (elementSize + padding)) - viewSize * rt.localScale.x;

        //stops going on left size
        if (rt.localPosition.x > padding+150)
            rt.localPosition = new Vector3(padding, rt.localPosition.y, rt.localPosition.z);
        else if (rt.localPosition.x < -contentSize) 
            rt.localPosition = new Vector3(-contentSize, rt.localPosition.y, rt.localPosition.z);
        //stops going on right size


    }
}
