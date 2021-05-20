using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterObjectsManager : MonoBehaviour
{
    public MyGameManager MyGameManager { get; set; }
    public GameObject CircleLetters3, CircleLetters4, CircleLetters5;

    private void Awake()
    {
        MyGameManager = GetComponent<MyGameManager>();
    }

    public Transform GetChildCircleLetters(int index, int PlayingLetterCountLevel)
    {
        switch (PlayingLetterCountLevel)
        {
            case 3:
                return CircleLetters3.transform.GetChild(index);

            case 4:
                return CircleLetters4.transform.GetChild(index);

            case 5:
                return CircleLetters5.transform.GetChild(index);

            default:
                return null;
        }
    }

    public string GetChildCircleLetters3Letter(int index)
    {
        return CircleLetters3.transform.GetChild(index).GetChild(0).GetComponent<Text>().text;
    }
}