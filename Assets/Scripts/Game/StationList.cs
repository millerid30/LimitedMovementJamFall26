using System.Collections.Generic;
using UnityEngine;

public class StationList : MonoBehaviour
{
    public static StationList Instance;
    public List<GameObject> stations;
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        if (stations.Count == 0)
        {
            FindStations();
        }
        foreach (Transform s in transform)
        {
            if (s.GetComponent<Station>() != null)
            {
                GameObject g = s.gameObject;
                stations.Add(g);
            }
        }
    }
    public void FindStations()
    {
        Station[] target = FindObjectsByType<Station>(FindObjectsInactive.Include);
        foreach (Station t in target)
        {
            t.transform.SetParent(transform);
        }
    }
}