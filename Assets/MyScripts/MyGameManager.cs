using System;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private int _wrongTry = 0;
    private bool _ısLevelCompleted;
    private TimeSpan _timePassed;
    private int _score;

    public User User { get; set; }
    public PointerManager PointerManager { get; set; }
    public LetterObjectsManager LetterObjectsManager { get; set; }
    public UIManager UIManager { get; set; }
    public Line Line { get; set; }
    public TextManager TextManager { get; set; }
    public bool LockLineDrawing { get; set; } = false;
    public Tuple<List<string>, string> playingWordSubwordL { get; set; }
    public int PlayingLetterCountLevel { get; set; } = 3;
    public int WrongTry
    {
        get => _wrongTry;
        set
        {
            _wrongTry = value;
            UIManager.textWrongTry.text = value.ToString();
        }
    }
    public TimeSpan TimePassed
    {
        get => _timePassed;
        set
        {
            _timePassed = value;
            UIManager.textTime.text = value.ToString(@"mm\:ss\.ff");
        }
    }
    public TimeSpan TimeCompleted { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            UIManager.textScore.text = "Score : " + value.ToString();
        }
    }
    public bool IsLevelCompleted
    {
        get => _ısLevelCompleted;
        set
        {
            _ısLevelCompleted = value;
            Invoke("SetActivePnlLevelCompleted", .75f);

            if (value == true)
            {
                //TextManager.WriteStringCompleted(playingWordSubwordL.Item2, PlayingLetterCountLevel);
                User.WriteStringCompletedUser(playingWordSubwordL.Item2, PlayingLetterCountLevel);
                TimeEnd = DateTime.Now;
                TimeCompleted = TimeEnd - TimeStart;
                Score = (int)CalculateScore();
            }
        }
    }

    private void SetActivePnlLevelCompleted()
    {
        UIManager.pnlLevelCompleted.SetActive(IsLevelCompleted);
    }

    private void Awake()
    {
        PointerManager = GetComponent<PointerManager>();
        LetterObjectsManager = GetComponent<LetterObjectsManager>();
        UIManager = GetComponent<UIManager>();
        Line = GetComponent<Line>();
        TextManager = GetComponent<TextManager>();
        User = GetComponent<User>();
    }

    private void Start()
    {
        //UIManager.ChangePage("PgWelcome");
        UIManager.ChangePage("PgLogin");
    }

    private void Update()
    {
        if (IsLevelCompleted == false)
        {
            TimePassed = DateTime.Now - TimeStart;
        }
    }

    public void PreparePageL(int letterCountLevel)
    {
        PlayingLetterCountLevel = letterCountLevel;
        WrongTry = 0;
        TimePassed = new TimeSpan();
        TimeStart = DateTime.Now;
        IsLevelCompleted = false;

        playingWordSubwordL = TextManager.GetRandomWordSubwords(letterCountLevel);

        UIManager.PreparePageL(letterCountLevel, playingWordSubwordL);
    }

    public bool CheckWord(string word)
    {
        foreach (string subword in playingWordSubwordL.Item1)
        {
            if (word.ToLower() == subword.ToLower())
            {
                return true;
            }
        }

        return false;
    }

    public float CalculateScore()
    {
        float seconds = 0;

        switch (PlayingLetterCountLevel)
        {
            case 3:
                seconds = UIManager.pnlWordsLetterCount * 1.2f;
                break;

            case 4:
                seconds = UIManager.pnlWordsLetterCount * 2f;
                break;

            case 5:
                seconds = UIManager.pnlWordsLetterCount * 15f;
                break;

            default:
                break;
        }

        if ((float)TimeCompleted.TotalSeconds < seconds)
        {
            return 100;
        }
        else if ((float)TimeCompleted.TotalSeconds > 3 * seconds)
        {
            return 0;
        }
        else
        {
            return 100 - (((float)TimeCompleted.TotalSeconds - seconds) / (2 * seconds)) * 100 - WrongTry * 5;
        }
    }
}