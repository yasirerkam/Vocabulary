using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    public MyGameManager MyGameManager { get; set; }
    public bool[] PointerDown3 { get; set; }
    public bool[] PointerEnter3 { get; set; }
    public bool[] PointerEnter4 { get; set; }
    public bool[] PointerEnter5 { get; set; }
    public int pointerDownAt { get; set; } = -1;
    public int pointerEnterAt { get; set; } = -1;

    private void Awake()
    {
        PointerDown3 = new bool[3];
        PointerEnter3 = new bool[3];
        PointerEnter4 = new bool[4];
        PointerEnter5 = new bool[5];
        MyGameManager = GetComponent<MyGameManager>();
    }

    private void Update()
    {
        switch (MyGameManager.PlayingLetterCountLevel)
        {
            case 3:
                PointerDown(PointerDown3);
                PointerEnter(PointerEnter3);
                break;

            case 4:
                PointerEnter(PointerEnter4);
                break;

            case 5:
                PointerEnter(PointerEnter5);
                break;

            default:
                break;
        }

        Debug.Log("pointerDownAt : " + pointerDownAt);
        Debug.Log("pointerEnterAt : " + pointerEnterAt);
    }

    private void PointerEnter(bool[] PointerEnter)
    {
        int j;
        for (j = 0; j < PointerEnter.Length; j++)
        {
            if (PointerEnter[j])
            {
                pointerEnterAt = j;
                break;
            }
        }
        if (j >= PointerEnter.Length)
        {
            pointerEnterAt = -1;
        }
    }

    private void PointerDown(bool[] PointerDown)
    {
        int i;
        for (i = 0; i < PointerDown.Length; i++)
        {
            if (PointerDown[i])
            {
                pointerDownAt = i;
                break;
            }
        }
        if (i >= PointerDown.Length)
        {
            pointerDownAt = -1;
        }
    }

    public void SetPointerDown3P1(bool b)
    {
        PointerDown3[0] = b;
    }

    public void SetPointerDown3P2(bool b)
    {
        PointerDown3[1] = b;
    }

    public void SetPointerDown3P3(bool b)
    {
        PointerDown3[2] = b;
    }

    public void SetPointerEnter3P1(bool b)
    {
        PointerEnter3[0] = b;
    }

    public void SetPointerEnter3P2(bool b)
    {
        PointerEnter3[1] = b;
    }

    public void SetPointerEnter3P3(bool b)
    {
        PointerEnter3[2] = b;
    }

    public void SetPointerEnter4P1(bool b)
    {
        PointerEnter4[0] = b;
    }

    public void SetPointerEnter4P2(bool b)
    {
        PointerEnter4[1] = b;
    }

    public void SetPointerEnter4P3(bool b)
    {
        PointerEnter4[2] = b;
    }

    public void SetPointerEnter4P4(bool b)
    {
        PointerEnter4[3] = b;
    }

    public void SetPointerEnter5P1(bool b)
    {
        PointerEnter5[0] = b;
    }

    public void SetPointerEnter5P2(bool b)
    {
        PointerEnter5[1] = b;
    }

    public void SetPointerEnter5P3(bool b)
    {
        PointerEnter5[2] = b;
    }

    public void SetPointerEnter5P4(bool b)
    {
        PointerEnter5[3] = b;
    }

    public void SetPointerEnter5P5(bool b)
    {
        PointerEnter5[4] = b;
    }
}