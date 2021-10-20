using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [SerializeField]
    PriorityList priorities;
    string filePath;

    void Awake()
    {
        filePath = Application.dataPath + "/progress.json";
        LoadPriorities();
    }

    private void Start()
    {
        FindObjectOfType<PlayerTriggersControl>().hideEvent += IncreasePriority;
    }

    public List<string> GetPriorities()
    {
        List<Priority> list = new List<Priority>();

        foreach (var p in priorities.list)
        {
            if (p.Value > 0)
            {
                list.Add(p);
            }
        }

        var namesList = list.OrderByDescending(x => x.Value).Select(x => x.Name).ToList();

        return namesList;
    }

    public void IncreasePriority(string tag)
    {
        Debug.Log("Increased " + tag + " by 1.");
        priorities.list.Where(x => x.Name == tag).First().Value++;
        SavePriorities();
    }

    public void LoadPriorities()
    {
        if (File.Exists(filePath))
        {
            string progressJson = File.ReadAllText(filePath);
            priorities = JsonUtility.FromJson<PriorityList>(progressJson);
        }
        else
        {
            priorities.list = new List<Priority>()
            {
                new Priority{Name="Vent", Value=0 },
                new Priority{Name="Closet", Value=0},
                new Priority{Name="Under", Value = 0 },
                new Priority{Name="Normal", Value =1},
            };
        }
    }

    public void SavePriorities()
    {
        string prioritiesJson = JsonUtility.ToJson(priorities);
        File.WriteAllText(filePath, prioritiesJson);
    }
}

[Serializable]
public class Priority
{
    public string Name;
    public int Value;
}

[Serializable]
public class PriorityList
{
    public List<Priority> list;
}