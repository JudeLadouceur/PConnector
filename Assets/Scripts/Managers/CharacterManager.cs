using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager: MonoBehaviour
{
    public SO_Character[] characters;
    
    public Dictionary<string, int> flags;

    public static CharacterManager instance;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        flags = new Dictionary<string, int>();

        foreach (SO_Character character in characters)
        {
            foreach (SO_Character.Heuristics heuristics in character.variables)
            {
                print(heuristics.name);
                print(heuristics.value);
                flags.TryAdd(heuristics.name, heuristics.value);
                print(flags.Count);
            }
        }
    }

    public void ResetFlags()
    {
        foreach (string flag in flags.Keys)
        {
            flags[flag] = 0;
        }
    }
}
