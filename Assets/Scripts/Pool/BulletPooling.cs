using UnityEngine;
using VavilichevGD.Utils.Timing;

namespace Pool
{
    public class BulletPooling : GameObjectsPooling
    {
        private float _bulletLifeTime;

        public BulletPooling(GameObject prefab, int startCount, Transform container, float bulletLifeTime, bool isAutoExpand) : base
            (prefab, startCount, container, isAutoExpand)
        {
            _bulletLifeTime = bulletLifeTime;
        }

        public void ShootBullet(Vector2 force, float scatterAmount)
        {
            var bullet = GetFreeElement(false);
            var player = GameObject.FindObjectOfType<Player>();
            bullet.transform.position = player.transform.position;
            bullet.transform.rotation = player.transform.rotation;
            bullet.SetActive(true);

            var scatter = new Vector2(Random.Range(1, scatterAmount),Random.Range(1, scatterAmount));
            bullet.GetComponent<Rigidbody2D>().AddForce(force * scatter);
            bullet.GetComponent<PoolObject>().Take(_bulletLifeTime);
        }
    }
}
