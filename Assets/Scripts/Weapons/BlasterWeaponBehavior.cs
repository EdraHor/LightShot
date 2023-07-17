using Pool;
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class BlasterWeaponBehavior : IWeaponBehavior
    {
        private Coroutine coroutine; //TODO : Upgrade to custom timer
        private Player _player;
        private WeaponSO _weapon;
        private BulletPooling pool;

        public void EnterState(Player player, WeaponSO weapon)//Второй параметр ссылка на оружие
        {
            _player = player;
            _weapon = weapon;
            pool = new BulletPooling(weapon.Specifications.BulletPrefab, 50, 
            GameObject.FindGameObjectWithTag("BulletsPool").transform, weapon.Specifications.BulletLifeTime, true);
            coroutine = Coroutines.Start(Shoot());
        }

        public void ExitState(Player player, WeaponSO weapon)
        {
            _player = null;
            _weapon = null;
            Coroutines.Stop(coroutine);
        }

        public void UpdateState()
        {
            //        bool isShootButton;
            //#if UNITY_STANDALONE
            //        isShootButton = Input.GetMouseButton(0));
            //#elif UNITY_ANDROID
            //        isShootButton = player.shotInput.isDown;
            //#endif
        }

        IEnumerator Shoot() //заменить на таймер
        {
            while (true)
            {
                if (_player.shotInput.isDown)
                {
                    var bulletForce = _weapon.Specifications.BulletSpeed * _player.shotInput.Value * 100;
                    pool.ShootBullet(bulletForce, _weapon.Specifications.ScatterAmount);

                    //var bullet = GameObject.Instantiate(_weapon.Specifications.BulletPrefab, 
                    //    _player.transform.position, _player.transform.rotation);



                    //bullet.GetComponent<Rigidbody2D>().AddForce(_weapon.Specifications.BulletSpeed * 100 * scatter);
                    //GameObject.Destroy(bullet, 5);
                }

                yield return new WaitForSeconds(_weapon.Specifications.ShootRare);
            }
        }
    }
}

