using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class TextManager : MonoBehaviour, ITextManager
{
    public MyGameManager MyGameManager { get; set; }
    public List<string> words { get; set; } = new List<string>();

    public List<string> words3l { get; set; } = new List<string>();
    public List<string> words4l { get; set; } = new List<string>();
    public List<string> words5l { get; set; } = new List<string>();
    public Dictionary<string, List<string>> wordSubwords3L { get; set; } = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> wordSubwords4L { get; set; } = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> wordSubwords5L { get; set; } = new Dictionary<string, List<string>>();

    private void Awake()
    {
        MyGameManager = GetComponent<MyGameManager>();

        ReadString(words);

        PrepareText();

        words3l = GetWordsNLetters(words, 3);
        words4l = GetWordsNLetters(words, 4);
        words5l = GetWordsNLetters(words, 5);
    }

    public void PrepareText()
    {
        foreach (string word in GetWordsNLetters(words, 3))
        {
            wordSubwords3L.Add(word, GetSubWordsAllLenght(words, word));
        }

        foreach (string word in GetWordsNLetters(words, 4))
        {
            wordSubwords4L.Add(word, GetSubWordsAllLenght(words, word));
        }

        foreach (string word in GetWordsNLetters(words, 5))
        {
            wordSubwords5L.Add(word, GetSubWordsAllLenght(words, word));
        }
    }

    #region File

    public void ReadString(List<string> words)
    {
        string path = @"Assets/Resources/WordsTR.txt";

        StreamReader reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            string w;
            words.Add(w = reader.ReadLine().Trim());
        }

        reader.Close();
    }

    public void WriteStringCompleted(string word, int playingLetterCountLevel)
    {
        string path = string.Format("Assets/Resources/WordsTRCompleted{0}L.txt", playingLetterCountLevel);

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

    public List<string> ReadStringCompleted(int playingLetterCountLevel)
    {
        string path = string.Format("Assets/Resources/WordsTRCompleted{0}L.txt", playingLetterCountLevel);
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

    #endregion File

    public List<string> GetWordsNLetters(List<string> words, int lettersN)
    {
        List<string> wordsNLetters = new List<string>();

        foreach (string word in words)
        {
            if (word.Length == lettersN && !wordsNLetters.Contains(word))
            {
                wordsNLetters.Add(word);
            }
        }

        return wordsNLetters;
    }

    public List<string> GetSubWords(List<string> wordsNletters, string word)
    {
        List<string> subWords = new List<string>();

        foreach (string wordN in wordsNletters)
        {
            List<char> wordCL = word.OfType<char>().ToList();

            int i = 0;
            for (i = 0; i < wordN.Length; i++)
            {
                if (wordCL.Contains(wordN[i]))
                {
                    wordCL.Remove(wordN[i]);
                }
                else break;
            }

            if (i == wordN.Length)
            {
                subWords.Add(wordN);
            }
        }

        return subWords;
    }

    public List<string> GetSubWordsAllLenght(List<string> words, string word)
    {
        List<string> subWordsAllLngt = new List<string>();

        for (int i = word.Length; i > 1; i--)
        {
            subWordsAllLngt.AddRange(GetSubWords(GetWordsNLetters(words, i), word));
        }

        return subWordsAllLngt;
    }

    public Tuple<List<string>, string> GetRandomWordSubwords(int letterCount)
    {
        switch (letterCount)
        {
            case 3:
                return GetRandomWordSubwords(words3l, wordSubwords3L, letterCount);

            case 4:
                return GetRandomWordSubwords(words4l, wordSubwords4L, letterCount);

            case 5:
                return GetRandomWordSubwords(words5l, wordSubwords5L, letterCount);

            default:
                return Tuple.Create<List<string>, string>(null, null);
        }
    }

    private Tuple<List<string>, string> GetRandomWordSubwords(List<string> wordsL, Dictionary<string, List<string>> wordSubwordsL, int letterCount)
    {
        for (int i = 0; i < wordsL.Count; i++)
        {
            int rnd = MyRandom.RandomWithExclusion(0, wordsL.Count);

            string word = wordsL[rnd];

            //if (wordSubwordsL[word].Count > 1 && !ReadStringCompleted(letterCount).Contains(word))
            if (wordSubwordsL[word].Count > 1 && !MyGameManager.User.ReadStringCompletedUser(letterCount).Contains(word))
            {
                return Tuple.Create(wordSubwordsL[word], word);
            }
        }

        return Tuple.Create<List<string>, string>(null, null);
    }

    public Dictionary<string, List<string>> GetWordHaveMT1Subwords(List<string> wordsL, Dictionary<string, List<string>> wordSubwordsL)
    {
        Dictionary<string, List<string>> wordHaveMT1Subwords = new Dictionary<string, List<string>>();

        for (int i = 0; i < wordsL.Count; i++)
        {
            string word = wordsL[i];

            if (wordSubwordsL[word].Count > 1)
            {
                wordHaveMT1Subwords.Add(word, wordSubwordsL[word]);
            }
        }

        return wordHaveMT1Subwords;
    }
}