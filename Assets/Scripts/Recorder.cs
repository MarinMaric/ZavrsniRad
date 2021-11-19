using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [SerializeField]
    StoredContents storedInfo;
    string filePath;

    void Awake()
    {
        string saveName = FindObjectOfType<MenuMaster>().saveName;
        filePath = Application.dataPath + "/"  + saveName + ".json";
        LoadPriorities();

        FindObjectOfType<CombatController>().dealtDamageEvent += IncreaseImmunity;
    }

    private void Start()
    {
        FindObjectOfType<HidingController>().hideEvent += IncreasePriority;
    }

    public List<StoredItem> GetPriorities()
    {
        List<StoredItem> list = new List<StoredItem>();

        foreach (var p in storedInfo.priorities)
        {
            if (p.Value > 0)
            {
                list.Add(p);
            }
        }

        var prioritiesList = list.OrderByDescending(x => x.Value).ToList();

        return prioritiesList;
    }

    public Dictionary<string, int> GetImmunities()
    {
        Dictionary<string, int> immunities = new Dictionary<string, int>();

        foreach (var i in storedInfo.immunities)
        {
            immunities.Add(i.Name, i.Value);
        }

        return immunities;
    }

    public void IncreasePriority(string tag)
    {
        Debug.Log("Increased " + tag + " by 1.");
        storedInfo.priorities.Where(x => x.Name == tag).First().Value++;
    }

    public void IncreaseImmunity(Item weapon)
    {
        Debug.Log("Increased immunity to " + weapon.name + " by 4%");
        storedInfo.immunities.Where(x => x.Name == weapon.name).First().Value+=4;
    }

    public void IncreaseImmunity(string name)
    {
        Debug.Log("Increased immunity to " + name + " by 4%");
        storedInfo.immunities.Where(x => x.Name == name).First().Value += 4;
    }

    public void IncreaseRound()
    {
        storedInfo.round++;
    }

    public void LoadPriorities()
    {
        if (File.Exists(filePath))
        {
            string progressJson = File.ReadAllText(filePath);
            storedInfo = JsonUtility.FromJson<StoredContents>(progressJson);
            GetComponent<GameMaster>().roundCount = storedInfo.round;
            GetComponent<GameMaster>().roundText.text = "ROUND: " + storedInfo.round.ToString();
        }
        else
        {
            storedInfo = new StoredContents();
            storedInfo.priorities = new List<StoredItem>()
            {
                new StoredItem{Name="Vent", Value=0 },
                new StoredItem{Name="Closet", Value=0},
                new StoredItem{Name="Under", Value = 0 },
                new StoredItem{Name="Normal", Value =1}
            };
            storedInfo.immunities = new List<StoredItem>() {
                new StoredItem{Name="Flamethrower", Value=0 },
                new StoredItem{Name="Shotgun", Value=0},
                new StoredItem{Name="Landmine", Value = 0 },
                new StoredItem{Name="Magnet", Value = 0},
                new StoredItem{Name="SlowDown", Value =0}
            };
            storedInfo.round = 1;
            GetComponent<GameMaster>().roundCount=1;
            GetComponent<GameMaster>().roundText.text = "ROUND: 1";
        }
    }

    public void SaveProgress()
    {
        string progressJson = JsonUtility.ToJson(storedInfo);
        File.WriteAllText(filePath, progressJson);
    }
}

[Serializable]
public class StoredItem
{
    public string Name;
    public int Value;
}

[Serializable]
public class StoredContents
{
    public List<StoredItem> priorities;
    public List<StoredItem> immunities;
    public int round;
}