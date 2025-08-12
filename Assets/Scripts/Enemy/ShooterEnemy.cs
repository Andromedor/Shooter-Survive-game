using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShooterEnemy : Enemy
    {
        [SerializeField] private float _fireRate = 2f;
        [SerializeField] private Transform _firePoint;

        private float _nextFireTime;
        private Transform _player;

        protected override void Start()
        {
            base.Start();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void GameUpdate()
        {
            if (_player == null) return;

            Vector2 direction = (_player.position - transform.position).normalized;
            transform.position += (Vector3)direction * _moveSpeed * Time.deltaTime;

            if (Time.time >= _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + _fireRate;
            }
        }

        private void Shoot()
        {
            Vector3 direction = (_player.position - _firePoint.position);
            direction.z = 0f;

            Bullet bullet = BulletPool.Instance.GetBullet();
            bullet.transform.position = _firePoint.position;
            bullet.Init(direction, "Player", _damage);
        }
    }
}
