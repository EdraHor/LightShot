using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    public SensorController movInput;
    public SensorController shotInput;

    public float MovementSpeed = 1f;
    private IWeaponBehavior currentWeaponBehaviour;
    private Dictionary<WeaponType, IWeaponBehavior> WeaponBehaviors;
    public WeaponSO CurrentWeapon { get; private set; }
    
    public Vector3 FireInput 
    { 
        get
        {
#if UNITY_STANDALONE
            var mousePositionOn2D = _camera.ScreenToWorldPoint(Input.mousePosition);
            var _angleToMouse = (mousePositionOn2D - transform.position).normalized;
            return _angleToMouse;
#elif UNITY_ANDROID
            return shotInput.Value;
#endif
        }
    }

    private void InitWeaponBehaviours(WeaponSO StartWeapon)
    {
        //???Для возможности иметь оружие отного типа, но с разным поведением поменять ключ на id оружия
        WeaponBehaviors = new Dictionary<WeaponType, IWeaponBehavior>()
        {
            { WeaponType.Blaster, new BlasterWeaponBehavior() },
            { WeaponType.Laser, new LaserWeaponBehavior() }
            //Add new weapons type here
        };

        ChangeWeapon(StartWeapon);
    }
    public void ChangeWeapon(WeaponSO weapon)
    {
        if (currentWeaponBehaviour!=null)
            currentWeaponBehaviour.ExitState(this, weapon);

        if (WeaponBehaviors.ContainsKey(weapon.Specifications.WeaponType))
        {
            CurrentWeapon = weapon;
            currentWeaponBehaviour = WeaponBehaviors[weapon.Specifications.WeaponType];
            currentWeaponBehaviour.EnterState(this, weapon);
        }
        else
        {
            Debug.LogError($"You try change weapon to not initialized weapon: {weapon}");
        }
    }

    void Start()
    {
        InitWeaponBehaviours(WeaponCollection.Weapons[0]);//TODO: ReworK to DictionaRy
    }

    void Update()
    {
        Movement();

        Rotate();

        currentWeaponBehaviour.UpdateState();
    }

    private void Movement()
    {
        PlayableArea.Instance.CheckContains(transform);
        var translation = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * MovementSpeed;
        transform.Translate(translation, Space.World);

#if UNITY_ANDROID
        if (movInput.isDown)
        {
            var translationMobile = movInput.Value * MovementSpeed;
            transform.Translate(translationMobile, Space.World);
        }
#endif
    }

    private void Rotate()
    {
        float rotation;
#if UNITY_STANDALONE
        rotation = Mathf.Atan2(_angleToMouse.y, _angleToMouse.x) * Mathf.Rad2Deg;
#elif UNITY_ANDROID
        rotation = Vector2.SignedAngle(Vector2.left, shotInput.Value);
#endif

        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        Debug.DrawRay(transform.position, FireInput * 5, Color.yellow);
    }

}
