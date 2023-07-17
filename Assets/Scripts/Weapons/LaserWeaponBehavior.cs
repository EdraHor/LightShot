using UnityEngine;

namespace Weapons
{
    public class LaserWeaponBehavior : IWeaponBehavior
    {
        private Player _player;
        private WeaponSO _weapon;
        private LineRenderer _lineRenderer;
        private GameObject _laserObject;

        public void EnterState(Player player, WeaponSO weapon)
        {
            _player = player;
            _weapon = weapon;
            _laserObject = GameObject.Instantiate(weapon.Specifications.BulletPrefab, _player.transform);
            _lineRenderer = _laserObject.GetComponent<LineRenderer>();
        }

        public void ExitState(Player player, WeaponSO weapon)
        {
            _player = null;
            _weapon = null;
            _lineRenderer = null;
            GameObject.Destroy(_laserObject);
        }

        public void UpdateState()
        {
            if (_player.shotInput.isDown)
            {
                _lineRenderer.enabled = true;
                Vector2 mousePos = _player.FireInput * Screen.width + _player.transform.position;

                _lineRenderer.SetPosition(0, _player.transform.position);
                _lineRenderer.SetPosition(1, mousePos);

                IDamagable damagableObject;

                var hit = Physics2D.Raycast(_player.transform.position, mousePos, 100,
                    _weapon.Specifications.ShootDetectionLayers);
                if (hit)
                {
                    if (hit.collider.TryGetComponent(out damagableObject))
                    {
                        _lineRenderer.SetPosition(0, _player.transform.position);
                        _lineRenderer.SetPosition(1, hit.point);
                        damagableObject.Kill();
                    }
                    else
                    {
                        _lineRenderer.SetPosition(0, _player.transform.position);
                        _lineRenderer.SetPosition(1, hit.point);
                    }
                }
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
    }
}

