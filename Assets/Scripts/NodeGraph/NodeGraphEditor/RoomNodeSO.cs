using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNode", menuName = "Scriptable Objects/Dungeon/Room Node")]
public class RoomNodeSO : ScriptableObject
{
    public string id;
    public List<string> parentRoomNodeIDList = new List<string>();
    public List<string> childRoomNodeIDList = new List<string>();
    [HideInInspector] public RoomNodeGraphSO roomNodeGraph;
    public RoomNodeTypeSO roomNodeType;
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;
    [HideInInspector] public bool isLeftClickDragging = false;
    [HideInInspector] public bool isSelected = false;

    #region Editor Code
#if UNITY_EDITOR
    [HideInInspector] public Rect rect;
    public void Initialize(Rect rect, RoomNodeGraphSO nodeGraph, RoomNodeTypeSO roomNodeType)
    {
        this.rect = rect;
        this.id = Guid.NewGuid().ToString();
        this.name = "RoomNode";
        this.roomNodeGraph = nodeGraph;
        this.roomNodeType = roomNodeType;

        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
    }

    public void Draw(GUIStyle nodeStyle)
    {
        //draw node box using begin area
        GUILayout.BeginArea(rect, nodeStyle);
        
        //start region to detect popup selection changes
        EditorGUI.BeginChangeCheck();
        
        //display a popup using the roomnodetype name values that can bi selected from(default to the currently set roomnodetype)
        int selected = roomNodeTypeList.list.FindIndex(x => x == roomNodeType);
        int selection = EditorGUILayout.Popup("", selected, GetRoomNodeTypesToDisplay());
        roomNodeType = roomNodeTypeList.list[selection];
        
        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(this);
        
        GUILayout.EndArea();
    }

    public string[] GetRoomNodeTypesToDisplay()
    {
        string[] roomArray = new string[roomNodeTypeList.list.Count];
        for (int i = 0; i < roomNodeTypeList.list.Count; ++i)
        {
            if (roomNodeTypeList.list[i].displayInNodeGraphEditor)
            {
                roomArray[i] = roomNodeTypeList.list[i].roomNodeTypeName;
            }
        }
        return roomArray;
    }
    
    public void ProcessEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;
            case EventType.MouseUp:
                ProcessMouseUpEvent(currentEvent);
                break;
            case EventType.MouseDrag:
                ProcessMouseDragEvent(currentEvent);
                break;
            default:
                break;
        }
    }

    private void ProcessMouseDragEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftMouseDragEvent(currentEvent);
        }
    }

    private void ProcessLeftMouseDragEvent(Event currentEvent)
    {
        isLeftClickDragging = true;

        DragNode(currentEvent.delta);
        GUI.changed = true;
    }

    private void DragNode(Vector2 delta)
    {
        rect.position += delta;
        EditorUtility.SetDirty(this);
    }

    private void ProcessMouseUpEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftClickUpEvent();
        }
    }

    private void ProcessLeftClickUpEvent()
    {
        if (isLeftClickDragging)
        {
            isLeftClickDragging = false;
        }
    }

    private void ProcessMouseDownEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftClickDownEvent();
        }
        else if (currentEvent.button == 1)
        {
            ProcessRightClickDownEvent(currentEvent);
        }
    }

    private void ProcessRightClickDownEvent(Event currentEvent)
    {
        roomNodeGraph.SetNodeToDrawConnectionLineFrom(this, currentEvent.mousePosition);
    }

    private void ProcessLeftClickDownEvent()
    {
        Selection.activeObject = this;
        
        //Toggle node selection
        if (isSelected)
        {
            isSelected = false;
        }
        else
        {
            isSelected = true;
        }
    }

    public bool AddChildRoomNodeIDToRoomNode(string childID)
    {
        childRoomNodeIDList.Add(childID);
        return true;
    }
    
    public bool AddParentRoomNodeIDToRoomNode(string childID)
    {
        parentRoomNodeIDList.Add(childID);
        return true;
    }
#endif
    #endregion
}
