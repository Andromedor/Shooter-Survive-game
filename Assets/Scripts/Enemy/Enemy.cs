using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Enemy : MonoBehaviour, IEntity
    {
        [SerializeField] protected int _maxHealth = 50;
        [SerializeField] protected int _damage = 10;
        [SerializeField] protected float _moveSpeed = 2f;


        protected int _currentHealth;
        protected Camera _cam;
        protected HealthBar _healthBar;


        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;
        public int Damage => _damage;
        public float MoveSpeed => _moveSpeed;
        public bool IsAlive => _currentHealth > 0;


        protected virtual void OnEnable()
        {
            GameUpdateManager.Instance.Register(this);
            TargetRegistry.RegisterEnemy(this);
        }


        protected virtual void OnDisable()
        {
            GameUpdateManager.Instance.Unregister(this);
            TargetRegistry.UnregisterEnemy(this);
        }


        protected virtual void Start()
        {
            _cam = Camera.main;
            _currentHealth = _maxHealth;
        }


        public void SetHealthBar(HealthBar hb)
        {
            _healthBar = hb;
            if (_healthBar != null)
            {
                _healthBar.SetMaxHealth(_maxHealth);
                _healthBar.SetHealth(_currentHealth);
                _healthBar.SetTarget(transform);
            }
        }

        public abstract void GameUpdate(float dt);


        public virtual void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth < 0) _currentHealth = 0;
            _healthBar?.SetHealth(_currentHealth);
            if (_currentHealth == 0) Die();
        }

        protected virtual void Die()
        {
            if (_healthBar != null) Destroy(_healthBar.gameObject);
            Destroy(gameObject);
        }
    }
}
