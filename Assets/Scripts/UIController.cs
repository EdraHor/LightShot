using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _inputPads;
    [SerializeField] private TextMeshProUGUI _enemyCounter;
    [SerializeField] private GameObject _menuPrefab;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _menuButtonBack;
    [SerializeField] private Slider _menuSliderScatter;
    [SerializeField] private Slider _menuSliderShootSpeed;
    [SerializeField] private Slider _menuSliderMovementSpeed;
    [SerializeField] private Slider _menuSliderBulletSpeed;
    [Space(15), Header("Weapon Selector")]
    [SerializeField] private TMP_Dropdown _weapons;


    private Player _player;
    private bool _isMenuOpen { get { return _menuPrefab.activeSelf; } }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _weapons.ClearOptions();

        foreach (var weaponName in WeaponCollection.Weapons)
        {
            _weapons.options.Add(new TMP_Dropdown.OptionData(weaponName.Info.Name));
        }

        _weapons.onValueChanged.AddListener((int value) => _player.ChangeWeapon(WeaponCollection.Weapons[value]));
        _menuButton.onClick.AddListener(OnMenuButtonDown);
        _menuButtonBack.onClick.AddListener(OnMenuButtonDown);
        _menuSliderScatter.onValueChanged.AddListener((float value) => _player.CurrentWeapon.Specifications.ScatterAmount = value);
        _menuSliderShootSpeed.onValueChanged.AddListener((float value) => _player.CurrentWeapon.Specifications.ShootRare = value);
        _menuSliderMovementSpeed.onValueChanged.AddListener((float value) => _player.MovementSpeed = value);
        _menuSliderBulletSpeed.onValueChanged.AddListener((float value) => _player.CurrentWeapon.Specifications.BulletSpeed = value);


        _menuPrefab.SetActive(false);
        EnemyController.Instance.EnemyDiedEvent += EnemyUpdateEvent;
        EnemyController.Instance.EnemySpawnEvent += EnemyUpdateEvent;
    }

    private void EnemyUpdateEvent()
    {
        _enemyCounter.text = $"Live: {EnemyController.Instance.EnemyLiveCount}  " +
                             $"Score: {EnemyController.Instance.Score}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    private void OpenMenu()
    {
        if (_isMenuOpen)
        {
            _inputPads.SetActive(true);
            _menuPrefab.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            _inputPads.SetActive(false);
            _menuPrefab.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnMenuButtonDown()
    {
        OpenMenu();
    }
}
