using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeType", menuName = "Scriptable Objects/Dungeon/Room Node Type")]
public class RoomNodeTypeSO : ScriptableObject
{
    public string roomNodeTypeName;

    #region Header
    [Header("Only flag that RoomNodeTypes that should be visible in the editor")]
    #endregion
    public bool displayInNodeGraphEditor = true;
    #region Header
    [Header("One Type should be a corridor")]
    #endregion
    public bool isCorridor = true;
    #region Header
    [Header("One Type should be a corridor north south")]
    #endregion
    public bool isCorridorNS = true;
    #region Header
    [Header("One Type should be a corridor east west")]
    #endregion
    public bool isCorridorEW = true;
    #region Header
    [Header("One Type should be an entrance")]
    #endregion
    public bool isEntrance = true;
    #region Header
    [Header("One Type should be a boos room")]
    #endregion
    public bool isBossRoom = true;
    #region Header
    [Header("One Type should be (unassigned)")]
    #endregion
    public bool isNone = true;
    
    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(roomNodeTypeName), roomNodeTypeName);
    }
#endif
    #endregion
        
}
