using Weapons;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("Resources")]
    public string WeaponsDestinationFolder = "ScriptableObjects/Weapons";

    private void Awake()
    {
        LoadResources();
    }

    private void LoadResources()
    {
        if (WeaponsDestinationFolder.Length == 0)
            Debug.LogError("You try load resources with null data path");
        WeaponCollection.Initialize(WeaponsDestinationFolder);
    }

}

