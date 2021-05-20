using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;

public class User : MonoBehaviour
{
    public MyGameManager MyGameManager { get; set; }
    public List<string> UserNames { get; set; }
    public GameObject PgLogin, GroupBtn, BtnUser, BtnOK;
    public string currentUser { get; set; }
    public Text Text;

    private void Awake()
    {
        MyGameManager = GetComponent<MyGameManager>();
        UserNames = ReadUserNames();
    }

    public void SaveNewUser(string userName)
    {
        string path = @"Assets/Resources/UserNames.txt";

        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(userName);
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(userName);
            }
        }
    }

    public List<string> ReadUserNames()
    {
        string path = @"Assets/Resources/UserNames.txt";
        List<string> vs = new List<string>();

        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
            }
        }
        else
        {
            using (StreamReader reader = File.OpenText(path))
            {
                while (!reader.EndOfStream)
                {
                    string w = reader.ReadLine().Trim();
                    vs.Add(w);
                }
            }
        }

        return vs;
    }

    public void SetPgLogin()
    {
        UserNames = ReadUserNames();

        for (int i = 0; i < GroupBtn.transform.childCount; i++)
        {
            Destroy(GroupBtn.transform.GetChild(i).gameObject);
        }

        foreach (string userName in UserNames)
        {
            GameObject btn = GameObject.Instantiate(BtnUser, GroupBtn.transform);

            btn.name = "btn" + userName;
            btn.GetComponent<Button>().onClick.AddListener(delegate { ChangePage(userName); });
            btn.transform.GetChild(0).GetComponent<Text>().text = userName;
        }
    }

    public void ChangePage(string userName)
    {
        currentUser = userName;
        MyGameManager.UIManager.ChangePage("PgWelcome");
    }

    public void Pressed()
    {
        SaveNewUser(Text.GetComponent<Text>().text.Trim());
        UserNames = ReadUserNames();
        SetPgLogin();
        MyGameManager.UIManager.ChangePage("PgWelcome");
    }

    public void WriteStringCompletedUser(string word, int playingLetterCountLevel)
    {
        string path = string.Format("Assets/Resources/WordsTRCompleted{1}{0}L.txt", playingLetterCountLevel, currentUser);

        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(word);
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(word);
            }
        }
    }

    public List<string> ReadStringCompletedUser(int playingLetterCountLevel)
    {
        string path = string.Format("Assets/Resources/WordsTRCompleted{1}{0}L.txt", playingLetterCountLevel, currentUser);
        List<string> vs = new List<string>();

        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
            }
        }
        else
        {
            using (StreamReader reader = File.OpenText(path))
            {
                while (!reader.EndOfStream)
                {
                    string w;
                    vs.Add(w = reader.ReadLine().Trim());
                }
            }
        }

        return vs;
    }
}