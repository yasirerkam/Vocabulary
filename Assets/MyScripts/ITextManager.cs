using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal interface ITextManager
{
    void ReadString(List<string> words);

    List<string> GetWordsNLetters(List<string> words, int lettersN);

    List<string> GetSubWords(List<string> wordsNletters, string word);

    List<string> GetSubWordsAllLenght(List<string> words, string word);
}