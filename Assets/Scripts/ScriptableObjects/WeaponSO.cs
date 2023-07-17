using System;
using UnityEngine;

namespace Weapons
{
    public enum WeaponType
    {
        Blaster,
        Laser
    }

    [CreateAssetMenu(fileName = "New weapon", menuName = "Weapon")]
    public class WeaponSO : ScriptableObject
    {
        public Info Info;
        public Specifications Specifications;
    }

    [Serializable]
    public class Info
    {
        [Header("Base Info")]
        public Sprite Image;
        public string Name;
        [TextArea(3, 5)]
        public string Discription;
        public float Cost;
    }

    [Serializable]
    public class Specifications
    {
        [Space(2), Header("Specifications")]
        public string WeaponID;
        public WeaponType WeaponType;
        public GameObject WeaponObject;
        public GameObject BulletPrefab;
        public float ShootRare;
        public float BulletSpeed;
        public float BulletLifeTime;
        [Min(1)]
        public float ScatterAmount;
        public LayerMask ShootDetectionLayers;
    }
}
