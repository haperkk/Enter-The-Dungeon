using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CurrentPlayer", menuName = "Scriptable Objects/Player/Current Player")]
public class CurrentPlayerSO : ScriptableObject
{
    [FormerlySerializedAs("playerDetailses")] public PlayerDetailsSO playerDetails;
    public string playerName;
}
