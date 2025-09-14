using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineTargetGroup))]
public class CinemachineTarget : MonoBehaviour
{
    private CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField] private Transform cursorTarget;

    private void Awake()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
    }

    private void Start()
    {
        SetcimachineTargetGroup();
    }

    private void SetcimachineTargetGroup()
    {
        CinemachineTargetGroup.Target cinemachineGroupTarget_Player = new CinemachineTargetGroup.Target{weight = 1.5f, radius = 1f, target = GameManager.Instance.GetPlayer().transform};
        CinemachineTargetGroup.Target cinemachineGroupTarget_Cursor = new CinemachineTargetGroup.Target{weight = 1f, radius = 1f, target = cursorTarget};
        CinemachineTargetGroup.Target[] cinemachineTargetArray = new CinemachineTargetGroup.Target[] {cinemachineGroupTarget_Player, cinemachineGroupTarget_Cursor};
        cinemachineTargetGroup.m_Targets = cinemachineTargetArray;
    }

    private void Update()
    {
        cursorTarget.position = HelperUtilities.GetMouseWorldPosition();
    }
}
