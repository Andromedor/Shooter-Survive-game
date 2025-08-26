using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BulletPool : MonoBehaviour
    {
        public static BulletPool Instance;
        [SerializeField] private Bullet _bulletPrefab;
        private readonly Queue<Bullet> _pool = new();


        private void Awake() { Instance = this; }


        public void Prewarm(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var b = Instantiate(_bulletPrefab);
                b.gameObject.SetActive(false);
                _pool.Enqueue(b);
            }
        }

        public void Spawn(Vector3 pos, Vector3 dir, bool fromPlayer, int damage)
        {
            Bullet b = _pool.Count > 0 ? _pool.Dequeue() : Instantiate(_bulletPrefab);
            b.transform.position = pos;
            b.Activate(dir, fromPlayer, damage);
        }


        public void Recycle(Bullet bullet)
        {
            GameUpdateManager.Instance.Unregister(bullet);
            bullet.gameObject.SetActive(false);
            _pool.Enqueue(bullet);
        }
    }
}