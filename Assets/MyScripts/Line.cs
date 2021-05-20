using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public MyGameManager MyGameManager { get; set; }
    public List<int> lettersDone { get; set; }

    public Material matLine;

    private LineRenderer lineRenderer;
    private Vector3 mousePos;
    private int currentLine;
    private List<GameObject> lines = new List<GameObject>();

    private void Awake()
    {
        MyGameManager = GetComponent<MyGameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && MyGameManager.LockLineDrawing == false && MyGameManager.PointerManager.pointerEnterAt > -1)
        {
            MyGameManager.LockLineDrawing = true;
            lettersDone = new List<int>();

            if (lineRenderer == null)
            {
                lines.Add(CreateLine());
            }

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 pos = MyGameManager.LetterObjectsManager.GetChildCircleLetters(MyGameManager.PointerManager.pointerEnterAt, MyGameManager.PlayingLetterCountLevel).position;
            pos.z = 0;

            lineRenderer.SetPosition(0, pos);
            lineRenderer.SetPosition(1, mousePos);

            lettersDone.Add(MyGameManager.PointerManager.pointerEnterAt);
        }
        else if (Input.GetMouseButtonUp(0) && lineRenderer)
        {
            if (MyGameManager.PointerManager.pointerEnterAt == -1)
            {
                Destroy(lines[lines.Count - 1]);
                lines.RemoveAt(lines.Count - 1);
            }
            else if (MyGameManager.PointerManager.pointerEnterAt > -1)
            {
                if (!lettersDone.Contains(MyGameManager.PointerManager.pointerEnterAt))
                {
                    //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //mousePos.z = 0;
                    Vector3 pos = MyGameManager.LetterObjectsManager.GetChildCircleLetters(MyGameManager.PointerManager.pointerEnterAt, MyGameManager.PlayingLetterCountLevel).position;
                    pos.z = 0;

                    lineRenderer.SetPosition(1, pos);
                    lineRenderer = null;
                    currentLine++;

                    lettersDone.Add(MyGameManager.PointerManager.pointerEnterAt);
                }
                else
                {
                    Destroy(lines[lines.Count - 1]);
                    lines.RemoveAt(lines.Count - 1);
                }
            }

            MyGameManager.LockLineDrawing = false;

            string wordLine = MyGameManager.UIManager.GetWordFromLetters(MyGameManager.PlayingLetterCountLevel, lettersDone);

            if (MyGameManager.CheckWord(wordLine))
            {
                MyGameManager.UIManager.SetVisibleWord(wordLine);

                MyGameManager.IsLevelCompleted = MyGameManager.UIManager.CheckAllVisible();
            }
            else
            {
                MyGameManager.WrongTry++;
            }

            ClearLines();
        }
        else if (Input.GetMouseButton(0) && lineRenderer)
        {
            if (MyGameManager.PointerManager.pointerEnterAt == -1)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                lineRenderer.SetPosition(1, mousePos);
            }
            else if (MyGameManager.PointerManager.pointerEnterAt > -1 && !lettersDone.Contains(MyGameManager.PointerManager.pointerEnterAt))
            {
                //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //mousePos.z = 0;
                Vector3 pos = MyGameManager.LetterObjectsManager.GetChildCircleLetters(MyGameManager.PointerManager.pointerEnterAt, MyGameManager.PlayingLetterCountLevel).position;
                pos.z = 0;

                lineRenderer.SetPosition(1, pos);
                lineRenderer = null;
                currentLine++;

                lines.Add(CreateLine());
                lineRenderer.SetPosition(0, pos);
                lineRenderer.SetPosition(1, pos);

                lettersDone.Add(MyGameManager.PointerManager.pointerEnterAt);
            }
        }
    }

    private GameObject CreateLine()
    {
        GameObject line = new GameObject("Line" + currentLine);
        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = matLine;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.useWorldSpace = false;
        lineRenderer.numCapVertices = 50;

        return line;
    }

    private void ClearLines()
    {
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }

        lines.Clear();
    }
}