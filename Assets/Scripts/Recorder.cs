using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    List<Priority> priorities;

    void Awake()
    {
        priorities = new List<Priority>()
        {
            new Priority{Name="Vent", Value=3 },
            new Priority{Name="Closet", Value=0},
            new Priority{Name="Under", Value = 0 },
            new Priority{Name="Normal", Value =4},
        };
    }

    public List<string> GetPriorities()
    {
        var list = priorities.Where(x => x.Value > 0).OrderByDescending(x => x.Value)
            .Select(x => x.Name).ToList();
 
        return list;
    }

    class Priority
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
