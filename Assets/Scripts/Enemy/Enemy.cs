using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Enemy : MonoBehaviour, IEntity, IDamageable, IUpdatable
    {
        [SerializeField] private int _maxHealth = 50;
        [SerializeField] protected int _damage = 10;
        [SerializeField] protected float _moveSpeed = 2f;
        [SerializeField] protected HealthBar _healthBar;

        public int MaxHealth { get => _maxHealth; set { _maxHealth = value; } }
        public int CurrentHealth { get => _currentHealth; set { _currentHealth = value; } }
        public int Damage { get => _damage; set { _damage = value; } }
        public float MoveSpeed { get => _moveSpeed; set { _moveSpeed = value; } }

        public HealthBar HealthBar { get => _healthBar; set { _healthBar = value; } }

        protected int _currentHealth;

        protected virtual void Start()
        {
            _currentHealth = _maxHealth;
            _healthBar?.SetMaxHealth(_maxHealth);
            GameUpdateManager.Instance.Register(this);
        }

        public abstract void GameUpdate();

        public virtual void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _healthBar?.SetHealth(_currentHealth);

            if (_currentHealth <= 0)
            {
                _healthBar.ActionDestroy?.Invoke();
                Die();
            }
        }

        protected virtual void Die()
        {
            GameUpdateManager.Instance.Unregister(this);
            Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (GameUpdateManager.Instance != null)
                GameUpdateManager.Instance.Unregister(this);
        }
    }
}
