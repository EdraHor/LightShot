using UnityEngine;

namespace Weapons
{
    public static class WeaponCollection
    {
        public static WeaponSO[] Weapons { get; private set; } //To Disctionaty and ID key

        public static void Initialize(string PathToWeaponsDestinationFolder)
        {
            Weapons = Resources.LoadAll<WeaponSO>(PathToWeaponsDestinationFolder);

            if (Weapons.Length == 0)
                Debug.LogWarning("Weapons not found or count equal zero");
        }

    }
}