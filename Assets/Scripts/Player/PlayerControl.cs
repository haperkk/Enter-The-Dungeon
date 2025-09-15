using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private MovementDetailsSO movementDetails;
    
    private Player player;
    private int currentWeaponIndex = 1;
    private float moveSpeed;
    
    // player roll
    private Coroutine playerRollCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate;
    private bool isPlayerRolling = false;
    private float playerRollCooldownTimer = 0f;
    
    private void Awake()
    {
        player = GetComponent<Player>();
        moveSpeed = movementDetails.GetMoveSpeed();
    }

    private void Start()
    {
        waitForFixedUpdate = new WaitForFixedUpdate();
        
        SetStartingWeapon();
        
        SetPlayerAnimationSpeed();
    }

    private void SetStartingWeapon()
    {
        int index = 1;
        foreach (Weapon weapon in player.weaponList)
        {
            if (weapon.weaponDetails == player.playerDetails.startingWeapon)
            {
                SetWeaponByIndex(index);
                break;
            }
        }
    }

    private void SetWeaponByIndex(int index)
    {
        if (index - 1 < player.weaponList.Count)
        {
            currentWeaponIndex = index;
            player.SetActiveWeaponEvent.CallSetActiveWeaponEvent(player.weaponList[index - 1]);
        }
        else
        {
            Debug.LogError("PlayerControl: SetWeaponByIndex: Index " + index + " is out of range of the weapon list.");
        }
    }

    private void Update()
    {
        if (isPlayerRolling) return;
        
        MovementInput();

        WeaponInput();

        PlayerRollCooldownTimer();
    }
    
    private void SetPlayerAnimationSpeed()
    {
        player.animator.speed = moveSpeed / Settings.baseSpeedForPlayerAnimations;
    }

    private void PlayerRollCooldownTimer()
    {
        if (playerRollCooldownTimer >= 0f)
        {
            playerRollCooldownTimer -= Time.deltaTime;
        }
    }

    private void MovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        bool rightMouseButtonDown = Input.GetMouseButtonDown(1);
        
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);
        
        if (horizontalMovement != 0f || verticalMovement != 0f)
        {
            direction *= 0.7f;
        }

        if (direction != Vector2.zero)
        {
            if (!rightMouseButtonDown)
            {
                player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);
            }
            else if (playerRollCooldownTimer <= 0f)
            {
                PlayRoll((Vector3)direction);
            }
        }
        else
        {
            player.idleEvent.CallIdleEvent();
        }
    }

    private void PlayRoll(Vector3 direction)
    {
        playerRollCoroutine = StartCoroutine(PlayerRollCoroutine(direction));
    }

    private IEnumerator PlayerRollCoroutine(Vector3 direction)
    {
        float minDistance = 0.2f;
        isPlayerRolling = true;
        
        Vector3 targetPosition = player.transform.position + (Vector3)direction * movementDetails.rollDistance;
        
        while(Vector3.Distance(player.transform.position, targetPosition) > minDistance)
        {
            player.movementToPositionEvent.CallMovementToPositionEvent(targetPosition, player.transform.position, movementDetails.rollSpeed, direction, isPlayerRolling);
            yield return waitForFixedUpdate;
        }
        
        isPlayerRolling = false;
        playerRollCooldownTimer = movementDetails.rollCooldownTime;
        player.transform.position = targetPosition;
    }

    private void WeaponInput()
    {
        Vector3 weaponDirection;
        float weaponAngleDegrees, playerAngleDegrees;
        AimDirection playerAimDirection;
         
        // Aim weapon input
        AimWeaponInput(out weaponDirection, out weaponAngleDegrees, out playerAngleDegrees, out playerAimDirection);
    }
    
    private void AimWeaponInput(out Vector3 weaponDirection, out float weaponAngleDegrees, out float playerAngleDegrees, out AimDirection playerAimDirection)
    {
        // Get mouse world position
        Vector3 mouseWorldPosition = HelperUtilities.GetMouseWorldPosition();

        // Calculate direction vector of mouse cursor from weapon shoot position
        weaponDirection = (mouseWorldPosition - player.activeWeapon.GetShootPosition());

        // Calculate direction vector of mouse cursor from player transform position
        Vector3 playerDirection = (mouseWorldPosition - transform.position);

        // Get weapon to cursor angle
        weaponAngleDegrees = HelperUtilities.GetAngleFromVector(weaponDirection);

        // Get player to cursor angle
        playerAngleDegrees = HelperUtilities.GetAngleFromVector(playerDirection);

        // Set player aim direction
        playerAimDirection = HelperUtilities.GetAimDirection(playerAngleDegrees);

        // Trigger weapon aim event
        player.aimWeaponEvent.CallAimWeaponEvent(playerAimDirection, playerAngleDegrees, weaponAngleDegrees, weaponDirection);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StopPlayerRollCoroutine();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        StopPlayerRollCoroutine();
    }
    
    private void StopPlayerRollCoroutine()
    {
        if (playerRollCoroutine != null)
        {
            StopCoroutine(playerRollCoroutine);
            isPlayerRolling = false;
        }
    }

    #region Validation

#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(movementDetails), movementDetails);
    }

#endif

    #endregion Validation
}
