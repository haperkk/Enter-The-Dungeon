using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


#region REQUIRE COMPONENTS
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(AimWeapon))]
[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(AnimatePlayer))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(MoveToPosition))]
[RequireComponent(typeof(SetActiveWeaponEvent))]
[RequireComponent(typeof(ActiveWeapon))]

[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
#endregion
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [HideInInspector] public Health health;
    [HideInInspector] public ActiveWeapon activeWeapon;
    
    [HideInInspector] public SetActiveWeaponEvent SetActiveWeaponEvent;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public AimWeaponEvent aimWeaponEvent;
    
    public List<Weapon> weaponList = new List<Weapon>();
    
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    
    private void Awake()
    {
        health = GetComponent<Health>();
        activeWeapon = GetComponent<ActiveWeapon>();
        
        SetActiveWeaponEvent = GetComponent<SetActiveWeaponEvent>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        idleEvent = GetComponent<IdleEvent>();
        aimWeaponEvent = GetComponent<AimWeaponEvent>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(PlayerDetailsSO playerDetails)
    {
        this.playerDetails = playerDetails;
        
        CreatePlayerStartingWeapons();
        
        SetPlayerHealth();
    }

    private void CreatePlayerStartingWeapons()
    {
        weaponList.Clear();

        foreach (WeaponDetailsSO weaponDetails in playerDetails.startingWeaponList)
        {
            AddWeaponToPlayer(weaponDetails);
        }
    }

    private Weapon AddWeaponToPlayer(WeaponDetailsSO weaponDetails)
    {
        Weapon weapon = new Weapon
        {
            weaponDetails = weaponDetails,
            weaponListPosition = weaponList.Count,
            weaponClipRemainingAmmo = weaponDetails.weaponClipAmmoCapacity,
            weaponRemainingAmmo = weaponDetails.weaponClipAmmoCapacity,
            isWeaponReloading = false
        };

        weaponList.Add(weapon);
        
        //todo: so the first weapon is 1? not 0?
        weapon.weaponListPosition = weaponList.Count;
        SetActiveWeaponEvent.CallSetActiveWeaponEvent(weapon);

        return weapon;
    }

    private void SetPlayerHealth()
    {
        health.SetStartingHealth(playerDetails.playerHealthAmount);
    }
}
