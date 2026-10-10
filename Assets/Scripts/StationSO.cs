using UnityEngine;

[CreateAssetMenu(fileName = "StationSO", menuName = "Scriptable Objects/StationSO")]
public class StationSO : ScriptableObject
{
    public string stationName;
    public string stationDescription;
    public string stationID;
    public StationType stationType;
    public float stationProgress;
    public float stationSpeed;
    public float stationDecay;
    public float stationChance;
}
public enum StationType
{
    Passive,
    Active
}