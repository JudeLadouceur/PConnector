using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveData 
{
    private const int defaultDay = 0;
    private const string defaultSceneNodeID = "";

    // We initialize all of the stats to be default values.
    public int day = defaultDay;
    public string sceneNodeID = defaultSceneNodeID;
    public List<DialogueVar> flagKeys = new List<DialogueVar>();
    public List<int> flagValues = new List<int>();

    public void WriteToFile(string filePath)
    {
        day = TimeManager.dayNumber;
        sceneNodeID = SceneManager.instance.currentSceneID;

        flagKeys.Clear();
        flagValues.Clear();
        foreach (DialogueVar flag in VariableManager.instance.flags.Keys)
        {
            flagKeys.Add(flag);
            flagValues.Add(VariableManager.instance.flags[flag]);
        }

        // Convert the instance ('this') of this class to a JSON string with "pretty print" (nice indenting).
        string json = JsonUtility.ToJson(this, true);

        // Write that JSON string to the specified file.
        File.WriteAllText(filePath, json);

        // Tell us what we just wrote if DEBUG_ON is on.
        Debug.LogFormat("WriteToFile({0}) -- data:\n{1}", filePath, json);
    }

    /// <summary>
    /// Returns a new SaveData object read from the data in the specified file.
    /// </summary>
    /// <param name="filePath">The file to attempt to read from.</param>
    public static SaveData ReadFromFile(string filePath)
    {
        // If the file doesn't exist then just return the default object.
        if (!File.Exists(filePath))
        {
            Debug.LogErrorFormat("ReadFromFile({0}) -- file not found, returning new object", filePath);
            return new SaveData();
        }
        else
        {
            // If the file does exist then read the entire file to a string.
            string contents = File.ReadAllText(filePath);

            // Tell us the file we read and its contents.
            Debug.LogFormat("ReadFromFile({0})\ncontents:\n{1}", filePath, contents);

            // If it happens that the file is somehow empty then tell us and return a new SaveData object.
            if (string.IsNullOrEmpty(contents))
            {
                Debug.LogErrorFormat("File: '{0}' is empty. Returning default SaveData");
                return new SaveData();
            }

            // Otherwise we can just use JsonUtility to convert the string to a new SaveData object.
            return JsonUtility.FromJson<SaveData>(contents);
        }
    }

    /// <summary>
    /// This is used to check if the SaveData object is the same as the default.
    /// i.e. it hasn't been written to yet.
    /// </summary>
    public bool IsDefault()
    {
        return (
            day == defaultDay);
    }

    /// <summary>
    /// A friendly string representation of this object.
    /// </summary>
    public override string ToString()
    {
        string[] flagKeyStrings = new string[flagKeys.Count];
        string[] flagValueStrings = new string[flagValues.Count];

        for (int i = 0; i<flagKeys.Count; i++)
        {
            flagKeyStrings[i] = flagKeys[i].ToString();
            flagValueStrings[i] = flagValues[i].ToString();
        }

        return string.Format(
            "day: {0}\nflag keys: {1}\nflag values: {2}",
            day,
            "[" + string.Join(",", flagKeyStrings) + "]",
            "[" + string.Join(",", flagValueStrings) + "]"
            );
    }
}
