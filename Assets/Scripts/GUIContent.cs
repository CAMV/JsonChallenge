using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

/// <summary>
/// Content tha a GUI matrix shows
/// </summary>
public class GUIContent 
{
    public string Title;
    public string[] ColumnHeaders;
    public string[,] Data;

    /// <summary>
    /// Initialize the values of the GUIContent with a JSON in Streaming Assets
    /// named "JsonChallenge.json"
    /// </summary>
    public void LoadFromJSON()
    {
        string filePath = Application.streamingAssetsPath;
        filePath += "/JsonChallenge.json";
        string fileContent = File.ReadAllText(filePath);

        JsonUtility.FromJsonOverwrite(fileContent, this);

        // Clean Data section

        char[] charsToTrim = {',', '\n', '\t', '}', '{', ']'};
        int startIndex = fileContent.IndexOf("Data");

        string dataString = fileContent.Substring(startIndex);
        dataString = dataString.Split('[')[1];

        string[] entries = dataString.Split('}');

        for (int i = 0; i < entries.Length-2; i++)
        {
            entries[i] = entries[i].Trim(charsToTrim);
        }

        // Get Data values
        Data = new string[ColumnHeaders.Length, entries.Length-2];
        string[] fields;

        for (int i = 0; i < entries.Length-2; i++)
        {
            fields = entries[i].Split(',');

            for (int j = 0; j < fields.Length; j++)
            {
                for (int k = 0; k < ColumnHeaders.Length; k++)
                {
                    if (fields[j].Contains(ColumnHeaders[k]))
                    {
                        Data[k,i] = fields[j].Split(':')[1];
                        Data[k,i] = Data[k,i].Split('\"')[1];
                    }
                }
            }
        }
        
    }

}