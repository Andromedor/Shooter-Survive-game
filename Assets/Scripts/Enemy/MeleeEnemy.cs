using System.Collections;
using UnityEngine;
namespace Assets.Scripts
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _attackCooldown = 1f;
        private float _lastAttackTime;
        private Transform _player;

        protected override void Start()
        {
            base.Start();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        public override void GameUpdate()
        {
            if (_player == null) return;

            float distance = Vector2.Distance(transform.position, _player.position);
            if (distance > _attackRange)
            {
                Vector2 direction = (_player.position - transform.position).normalized;
                transform.position += (Vector3)direction * _moveSpeed * Time.deltaTime;
            }
            else
            {
                TryAttack();
            }
        }

        private void TryAttack()
        {
            if (Time.time - _lastAttackTime >= _attackCooldown)
            {
                _player.GetComponent<IDamageable>()?.TakeDamage(_damage);
                _lastAttackTime = Time.time;
            }
        }
    }
}