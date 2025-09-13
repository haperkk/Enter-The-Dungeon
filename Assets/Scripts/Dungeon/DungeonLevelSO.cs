using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLevel_", menuName = "Scriptable Objects/Dungeon/Dungeon Level")]
public class DungeonLevelSO : ScriptableObject
{
    public string levelName;
    public List<RoomTemplateSO> roomTemplateList;
    public List<RoomNodeGraphSO> roomNodeGraphList;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(levelName), levelName);
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomTemplateList), roomTemplateList)) return;
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomNodeGraphList), roomNodeGraphList)) return;

        bool isEWCorridor = false;
        bool isNSCorridor = false;
        bool isEntrance = false;

        foreach (var roomTemplateSO in roomTemplateList)
        {
            if (roomTemplateSO == null) return;
            if (roomTemplateSO.roomNodeType.isEntrance) isEntrance = true;
            if (roomTemplateSO.roomNodeType.isCorridorEW) isEWCorridor = true;
            if (roomTemplateSO.roomNodeType.isCorridorNS) isNSCorridor = true;
        }
        if (isEntrance == false) Debug.Log("In " + this.name.ToString() + " : no entrance room type specified");
        if (isEWCorridor == false) Debug.Log("In " + this.name.ToString() + " : no east-west corridor room type specified");
        if (isNSCorridor == false) Debug.Log("In " + this.name.ToString() + " : no north-south corridor room type specified");

        foreach (var roomNodeGraph in roomNodeGraphList)
        {
            if (roomNodeGraph == null) return;
            foreach (var roomNodeSo in roomNodeGraph.roomNodeList)
            {
                if (roomNodeSo == null) return;
                
                if (roomNodeSo.roomNodeType.isEntrance || roomNodeSo.roomNodeType.isCorridorEW || roomNodeSo.roomNodeType.isCorridorNS || roomNodeSo.roomNodeType.isCorridor || roomNodeSo.roomNodeType.isNone) continue;
                
                bool isRoomTemplateTypeFound = false;
                
                foreach (var roomTemplateSO in roomTemplateList)
                {
                    if (roomTemplateSO == null) return;
                    if (roomNodeSo.roomNodeType == roomTemplateSO.roomNodeType)
                    {
                        isRoomTemplateTypeFound = true;
                        break;
                    }
                }
                
                if(!isRoomTemplateTypeFound) Debug.Log("In " + this.name.ToString() + " : no room template found for room node type " + roomNodeSo.roomNodeType.roomNodeTypeName.ToString());
            }
        }
    }
#endif
    #endregion
}
