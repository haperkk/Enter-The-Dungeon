using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetail", menuName = "Scriptable Objects/Player/Player Detail")]
public class PlayerDetailsSO : ScriptableObject
{
    public string playerCharacterName;
    public GameObject playerPrefab;
    public RuntimeAnimatorController RuntimeAnimatorController;
    public int playerHealthAmount;
    public Sprite playerMinimapIcon;
    
    // hand for holding weapons
    public Sprite playerHandSprite;

    #region VALIDATION

#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(playerCharacterName), playerCharacterName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerPrefab), playerPrefab);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(playerHealthAmount), playerHealthAmount);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerMinimapIcon), playerMinimapIcon);
        HelperUtilities.ValidateCheckNullValue(this, nameof(playerHandSprite), playerHandSprite);
        HelperUtilities.ValidateCheckNullValue(this, nameof(RuntimeAnimatorController), RuntimeAnimatorController);
    }
#endif

    #endregion
}
