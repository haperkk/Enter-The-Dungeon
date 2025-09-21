using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources"); 
            }

            return instance;
        }
    }
    
    #region Header DUNGEON
    [Space(10)]
    [Header("DUNGEON")]
    #endregion
    #region Tooltip
    [Tooltip("Populate with the dungeon RoomNodeTypeListSO")]
    #endregion
    public RoomNodeTypeListSO roomNodeTypeList;
    
    public Material dimmedMaterial;
    public Material litMaterial;
    public Shader variableLitShader;
    
    //Astar
    public TileBase[] enemyUnwalkableCollisionTilesArray;
    public TileBase preferredEnemyPathTile;
    
    //This is used to reference the current player between scenes
    public CurrentPlayerSO currentPlayer;
    
    //audio
    public AudioMixerGroup soundsMasterMixerGroup; 
    public SoundEffectSO doorOpenCloseSoundEffect;
    
    //UI
    public GameObject ammoIconPrefab;
    public GameObject heartPrefab;
    
    
        #region Validation
#if UNITY_EDITOR
    // Validate the scriptable object details entered
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(roomNodeTypeList), roomNodeTypeList);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(playerSelectionPrefab), playerSelectionPrefab);
        // HelperUtilities.ValidateCheckEnumerableValues(this, nameof(playerDetailsList), playerDetailsList);
        HelperUtilities.ValidateCheckNullValue(this, nameof(currentPlayer), currentPlayer);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(mainMenuMusic), mainMenuMusic);
        HelperUtilities.ValidateCheckNullValue(this, nameof(soundsMasterMixerGroup), soundsMasterMixerGroup);
        HelperUtilities.ValidateCheckNullValue(this, nameof(doorOpenCloseSoundEffect), doorOpenCloseSoundEffect);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(tableFlip), tableFlip);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(chestOpen), chestOpen);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(healthPickup), healthPickup);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(ammoPickup), ammoPickup);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(weaponPickup), weaponPickup);
        HelperUtilities.ValidateCheckNullValue(this, nameof(litMaterial), litMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(dimmedMaterial), dimmedMaterial);
        HelperUtilities.ValidateCheckNullValue(this, nameof(variableLitShader), variableLitShader);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(materializeShader), materializeShader);
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(enemyUnwalkableCollisionTilesArray), enemyUnwalkableCollisionTilesArray);
        HelperUtilities.ValidateCheckNullValue(this, nameof(preferredEnemyPathTile), preferredEnemyPathTile);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(musicMasterMixerGroup), musicMasterMixerGroup);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(musicOnFullSnapshot), musicOnFullSnapshot);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(musicLowSnapshot), musicLowSnapshot);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(musicOffSnapshot), musicOffSnapshot); 
        // HelperUtilities.ValidateCheckNullValue(this, nameof(heartPrefab), heartPrefab);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoIconPrefab), ammoIconPrefab);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(scorePrefab), scorePrefab);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(chestItemPrefab), chestItemPrefab);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(heartIcon), heartIcon);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(bulletIcon), bulletIcon);
        // HelperUtilities.ValidateCheckNullValue(this, nameof(minimapSkullPrefab), minimapSkullPrefab);
    }

#endif
    #endregion
}
