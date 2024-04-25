using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class WordManager : MonoBehaviour
{
    private string databasePath = @"Assets/WordleDB.json";
    private string selectedWord;


    private string GetRandomWordFromDatabase()
    {
        string randomWord = null;

        string jsonText = File.ReadAllText(databasePath);

        WordDataList wordDataList = JsonUtility.FromJson<WordDataList>(jsonText);

        if (wordDataList != null && wordDataList.words.Count > 0)
        {
            int randomIndex = Random.Range(0, wordDataList.words.Count);
            randomWord = wordDataList.words[randomIndex].kelime;
        }

        return randomWord;
    }

    private string GetSelectedWord()
    {
        if (string.IsNullOrEmpty(selectedWord))
        {
            selectedWord = GetRandomWordFromDatabase();
        }
        return selectedWord;
    }

    [System.Serializable]
    public class WordData
    {
        public string kelime;
    }

    [System.Serializable]
    public class WordDataList
    {
        public List<WordData> words;
    }

    public List<State> GetStates(string msg)
    {
        var result = new List<State>();

        var list = GetSelectedWord().ToCharArray().ToList();
        var listCurrent = msg.ToCharArray().ToList();

        for (var i = 0; i < listCurrent.Count; i++)
        {
            var currentChar = listCurrent[i];
            var contains = list.Contains(currentChar);
            if (contains)
            {
                var index = list.IndexOf(currentChar);
                var isCorrect = index == i; 
                result.Add(isCorrect ? State.Correct : State.Contain);
                list[index] = ' '; 
            }
            else
            {
                result.Add(State.None);
            }
        }

        for (var i = 0; i < listCurrent.Count; i++)
        {
            if (result[i] == State.None)
            {
                result[i] = State.Fail;
            }
        }

        return result;
    }
} 