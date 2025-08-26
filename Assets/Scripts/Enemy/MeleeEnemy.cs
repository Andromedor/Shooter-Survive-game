using System.Collections;
using UnityEngine;
namespace Assets.Scripts
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private float _attackRange = 1.2f;
        [SerializeField] private float _attackCooldown = 1f;
        private float _attackTimer;


        private Transform _playerTf;


        public override void GameUpdate(float dt)
        {
            if (!IsAlive) return;
            if (_playerTf == null)
                _playerTf = (TargetRegistry.Player as MonoBehaviour)?.transform;
            if (_playerTf == null) return;


            // Рух до гравця
            Vector3 dir = (_playerTf.position - transform.position).normalized;
            transform.position += dir * (_moveSpeed * dt);


            // Атака, якщо близько
            _attackTimer -= dt;
            if (_attackTimer <= 0f && Vector3.Distance(transform.position, _playerTf.position) <= _attackRange)
            {
                (TargetRegistry.Player as IDamageable)?.TakeDamage(_damage);
                _attackTimer = _attackCooldown;
            }
        }
    }
}