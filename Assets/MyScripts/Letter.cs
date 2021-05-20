using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public Material mat;
    private LineRenderer line;
    private Vector3 mousePos;
    private int currentLine;
    public bool pointerDown { get; set; }
    public MyGameManager MyGameManager { get; set; }

    private void Awake()
    {
        MyGameManager = GameObject.Find("MyGameManager").GetComponent<MyGameManager>();
    }
}