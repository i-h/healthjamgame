using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtManager : MonoBehaviour {
    public static List<Dirt> DirtList;
    public static void AddDirt(Dirt d)
    {
        if (DirtList == null) DirtList = new List<Dirt>();
        DirtList.Add(d);
    }
    public static void ClearDirt(Dirt d)
    {
        DirtList.Remove(d);
        Destroy(d.gameObject);
    }
    public static Status GetTotalDirt()
    {
        Status s = new Status();
        foreach(Dirt d in DirtList)
        {
            s.Current += d.Health;
            s.Max += d.MaxHealth;
        }
        s.Progress = s.Current / s.Max;
        if (DirtList.Count == 0) s.Progress = 0;
        return s;
    }
    public struct Status
    {
        public float Current;
        public float Max;
        public float Progress;
    }
}
