using UnityEngine;

[CreateAssetMenu(fileName = "Parameter/CreateScriptableObject/BoostParameter")]
public class BoostParameter : ScriptableObject
{
    public float maxBoostValue = 0;
    public float consumptionBoostValue = 0;
    public float recoveryTimeValue = 0;
    public float recoveryValue = 0;
    public float consumptionTimeValue = 0;
    public string boostName { get; private set; } = "boost";
}