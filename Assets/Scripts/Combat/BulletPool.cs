using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private int _poolSize = 20;

        public static BulletPool Instance;
        private Queue<Bullet> _bullets = new();

        private void Awake()
        {
            Instance = this;

            for (int i = 0; i < _poolSize; i++)
            {
                GameObject obj = Instantiate(_bulletPrefab);
                obj.SetActive(false);
                Bullet bullet = obj.GetComponent<Bullet>();
                _bullets.Enqueue(bullet);
            }
        }

        public Bullet GetBullet()
        {
            if (_bullets.Count > 0)
            {
                Bullet bullet = _bullets.Dequeue();
                bullet.gameObject.SetActive(true);
                return bullet;
            }
            else
            {
                GameObject obj = Instantiate(_bulletPrefab);
                return obj.GetComponent<Bullet>();
            }
        }

        public void ReturnBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            _bullets.Enqueue(bullet);
        }
    }
}