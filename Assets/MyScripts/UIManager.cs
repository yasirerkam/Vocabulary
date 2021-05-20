using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public MyGameManager MyGameManager { get; set; }
    public GameObject pgWelcome, pgGameIn, circleLetters, circleLetters3, circleLetters4, circleLetters5, groupBrnLetterWords;
    public GameObject pnlWords, imgLetterBG, pnlLevelCompleted;
    public Text textWrongTry, textTime, textScore;
    public int pnlWordsLetterCount { get; set; }

    private void Awake()
    {
        MyGameManager = GetComponent<MyGameManager>();
    }

    private void Start()
    {
    }

    public void PreparePageL(int letterCountLevel, Tuple<List<string>, string> randomSubwords)
    {
        SetPnlWords(randomSubwords.Item1);

        SetCircleLetters(letterCountLevel, randomSubwords.Item2);
    }

    public string GetWordFromLetters(int letterCountLevel, List<int> lettersOnUI)
    {
        Transform circleLettersXL = circleLetters.transform.GetChild(letterCountLevel - 3);
        string word = "";

        foreach (int letterIndex in lettersOnUI)
        {
            word += circleLettersXL.GetChild(letterIndex).GetChild(0).GetComponent<Text>().text;
        }

        return word;
    }

    public void ChangePage(string pageName)
    {
        MyGameManager.User.PgLogin.SetActive(false);

        pgWelcome.SetActive(false);
        pgGameIn.SetActive(false);
        pnlLevelCompleted.SetActive(false);
        circleLetters3.SetActive(false);
        circleLetters4.SetActive(false);
        circleLetters5.SetActive(false);

        switch (pageName)
        {
            case "PgLogin":
                MyGameManager.User.PgLogin.SetActive(true);
                MyGameManager.User.SetPgLogin();
                break;

            case "PgWelcome":
                pgWelcome.SetActive(true);
                SetPageWelc();
                break;

            case "PgGameIn3L":
                pgGameIn.SetActive(true);
                circleLetters3.SetActive(true);
                MyGameManager.PreparePageL(3);
                break;

            case "PgGameIn4L":
                pgGameIn.SetActive(true);
                circleLetters4.SetActive(true);
                MyGameManager.PreparePageL(4);
                break;

            case "PgGameIn5L":
                pgGameIn.SetActive(true);
                circleLetters5.SetActive(true);
                MyGameManager.PreparePageL(5);
                break;

            default:
                break;
        }
    }

    public void SetPageWelc()
    {
        groupBrnLetterWords.transform.GetChild(0).GetChild(0).GetComponent<Text>().text =
            //string.Format("3 Letters ({0} / {1})", MyGameManager.TextManager.ReadStringCompleted(3).Count, MyGameManager.TextManager.GetWordHaveMT1Subwords(MyGameManager.TextManager.words3l, MyGameManager.TextManager.wordSubwords3L).Count);
            string.Format("3 Letters ({0} / {1})", MyGameManager.User.ReadStringCompletedUser(3).Count, MyGameManager.TextManager.GetWordHaveMT1Subwords(MyGameManager.TextManager.words3l, MyGameManager.TextManager.wordSubwords3L).Count);
        groupBrnLetterWords.transform.GetChild(1).GetChild(0).GetComponent<Text>().text =
            //string.Format("4 Letters ({0} / {1})", MyGameManager.TextManager.ReadStringCompleted(4).Count, MyGameManager.TextManager.GetWordHaveMT1Subwords(MyGameManager.TextManager.words4l, MyGameManager.TextManager.wordSubwords4L).Count);
            string.Format("4 Letters ({0} / {1})", MyGameManager.User.ReadStringCompletedUser(4).Count, MyGameManager.TextManager.GetWordHaveMT1Subwords(MyGameManager.TextManager.words4l, MyGameManager.TextManager.wordSubwords4L).Count);
        groupBrnLetterWords.transform.GetChild(2).GetChild(0).GetComponent<Text>().text =
            //string.Format("5 Letters ({0} / {1})", MyGameManager.TextManager.ReadStringCompleted(5).Count, MyGameManager.TextManager.GetWordHaveMT1Subwords(MyGameManager.TextManager.words5l, MyGameManager.TextManager.wordSubwords5L).Count);
            string.Format("5 Letters ({0} / {1})", MyGameManager.User.ReadStringCompletedUser(5).Count, MyGameManager.TextManager.GetWordHaveMT1Subwords(MyGameManager.TextManager.words5l, MyGameManager.TextManager.wordSubwords5L).Count);
    }

    #region PnlWorld

    private void SetPnlWords(List<string> wordSubwords)
    {
        ClearPnlWord();
        pnlWordsLetterCount = 0;

        int sbwC = wordSubwords.Count;

        if (sbwC > 5)
            sbwC = 5;

        for (int i = 0; i < sbwC; i++)
        {
            for (int j = 0; j < wordSubwords[i].Length; j++)
            {
                GameObject gameObject = Instantiate(imgLetterBG, pnlWords.transform.GetChild(i));
                gameObject.transform.GetChild(0).GetComponent<Text>().text = wordSubwords[i][j].ToString().ToUpper();
                gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
                pnlWordsLetterCount++;
            }
        }
    }

    private string GetPnlWord(int index)
    {
        Transform transform = pnlWords.transform.GetChild(index);
        string word = "";

        for (int i = 0; i < transform.childCount; i++)
        {
            word += transform.GetChild(i).GetChild(0).GetComponent<Text>().text;
        }

        return word;
    }

    private void ClearPnlWord()
    {
        for (int index = 0; index < pnlWords.transform.childCount; index++)
        {
            Transform transform = pnlWords.transform.GetChild(index);

            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    public int SearchOnPnlWorld(string word)
    {
        for (int i = 0; i < pnlWords.transform.childCount; i++)
        {
            if (word == GetPnlWord(i))
            {
                return i;
            }
        }

        return -1;
    }

    public bool SetVisibleWord(string word)
    {
        int index = SearchOnPnlWorld(word);

        if (index > -1)
        {
            Transform transform = pnlWords.transform.GetChild(index);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<Text>().enabled = true;
            }

            return true;
        }
        else return false;
    }

    public bool CheckAllVisible()
    {
        int i;

        for (i = 0; i < pnlWords.transform.childCount; i++)
        {
            Transform transform = pnlWords.transform.GetChild(i);

            if (transform.childCount > 0)
                if (transform.GetChild(0).GetChild(0).GetComponent<Text>().enabled != true)
                    return false;
        }

        return pnlWords.transform.childCount == i ? true : false;
    }

    #endregion PnlWorld

    #region CircleLetters

    private void SetCircleLetters(int letterCountLevel, string word)
    {
        Transform circleLettersXL = circleLetters.transform.GetChild(letterCountLevel - 3);

        for (int i = 0; i < circleLettersXL.childCount; i++)
        {
            circleLettersXL.GetChild(i).GetChild(0).GetComponent<Text>().text = word[i].ToString().ToUpper();
        }

        SwitchCircleLetters();
    }

    public void SwitchCircleLetters()
    {
        Transform circleLettersXL = circleLetters.transform.GetChild(MyGameManager.PlayingLetterCountLevel - 3);

        for (int i = 0; i < circleLettersXL.childCount; i++)
        {
            int rnd = UnityEngine.Random.Range(0, MyGameManager.PlayingLetterCountLevel);
            string temp = circleLettersXL.GetChild(i).GetChild(0).GetComponent<Text>().text;

            circleLettersXL.GetChild(i).GetChild(0).GetComponent<Text>().text = circleLettersXL.GetChild(rnd).GetChild(0).GetComponent<Text>().text;
            circleLettersXL.GetChild(rnd).GetChild(0).GetComponent<Text>().text = temp;
        }
    }

    #endregion CircleLetters
}