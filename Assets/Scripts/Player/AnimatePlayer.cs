using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        // Load components
        player = GetComponent<Player>();
    }
    
    private void OnEnable()
    {
        player.movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;
        player.movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        
        // Subscribe to idle event
        player.idleEvent.OnIdle += IdleEvent_OnIdle;

        // Subscribe to weapon aim event
        player.aimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
    }

    private void OnDisable()
    {
        player.movementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;
        player.movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        
        // Unsubscribe from idle event
        player.idleEvent.OnIdle -= IdleEvent_OnIdle;

        // Unsubscribe from weapon aim event 
        player.aimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
    }
    
    private void MovementToPositionEvent_OnMovementToPosition(MovementToPositionEvent movementToPositionEvent, MovementToPositionArgs movementToPositionArgs)
    {
        InitializeAimAnimationParameters();
        InitializeRollAnimationParameters();
        SetMovementToPositionAnimationParameters(movementToPositionArgs);
    }
    
    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityArgs movementByVelocityArgs)
    {
        InitializeRollAnimationParameters();
        SetMovementAnimationParameters();
    }
    
    private void SetMovementAnimationParameters()
    {
        player.animator.SetBool(Settings.isMoving, true);
        player.animator.SetBool(Settings.isIdle, false);
    }
    
    private void SetMovementToPositionAnimationParameters(MovementToPositionArgs movementToPositionArgs)
    {
        // Animate roll
        if (movementToPositionArgs.isRolling)
        {
            if (movementToPositionArgs.moveDirection.x > 0f)
            {
                player.animator.SetBool(Settings.rollRight, true);
            }
            else if (movementToPositionArgs.moveDirection.x < 0f)
            {
                player.animator.SetBool(Settings.rollLeft, true);
            }
            else if (movementToPositionArgs.moveDirection.y > 0f)
            {
                player.animator.SetBool(Settings.rollUp, true);
            }
            else if (movementToPositionArgs.moveDirection.y < 0f)
            {
                player.animator.SetBool(Settings.rollDown, true);
            }
        }
    }
    
    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        InitializeRollAnimationParameters();
        SetIdleAnimationParameters();
    }

    private void SetIdleAnimationParameters()
    {
        player.animator.SetBool(Settings.isMoving, false);
        player.animator.SetBool(Settings.isIdle, true);
    }

    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs aimWeaponEventArgs)
    {
        InitializeAimAnimationParameters();
        InitializeRollAnimationParameters();
        SetAimWeaponAnimationParameters(aimWeaponEventArgs.aimDirection);
    }
    
    private void InitializeAimAnimationParameters()
    {
        player.animator.SetBool(Settings.aimUp, false);
        player.animator.SetBool(Settings.aimUpRight, false);
        player.animator.SetBool(Settings.aimUpLeft, false);
        player.animator.SetBool(Settings.aimRight, false);
        player.animator.SetBool(Settings.aimLeft, false);
        player.animator.SetBool(Settings.aimDown, false);
    }
    
    private void InitializeRollAnimationParameters()
    {
        player.animator.SetBool(Settings.rollDown, false);
        player.animator.SetBool(Settings.rollRight, false);
        player.animator.SetBool(Settings.rollLeft, false);
        player.animator.SetBool(Settings.rollUp, false);
    }
    
    private void SetAimWeaponAnimationParameters(AimDirection aimDirection)
    {
        // Set aim direction
        switch (aimDirection)
        {
            case AimDirection.up:
                player.animator.SetBool(Settings.aimUp, true);
                break;

            case AimDirection.upRight:
                player.animator.SetBool(Settings.aimUpRight, true);
                break;

            case AimDirection.upLeft:
                player.animator.SetBool(Settings.aimUpLeft, true);
                break;

            case AimDirection.right:
                player.animator.SetBool(Settings.aimRight, true);
                break;

            case AimDirection.left:
                player.animator.SetBool(Settings.aimLeft, true);
                break;

            case AimDirection.down:
                player.animator.SetBool(Settings.aimDown, true);
                break;

        }

    }
}
