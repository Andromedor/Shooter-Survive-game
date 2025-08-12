using UnityEngine;
namespace Assets.Scripts
{
    public class Player : MonoBehaviour, IEntity, IDamageable, IUpdatable
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _damage = 20;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private GameObject _bulletPrefab;

        public int MaxHealth { get => _maxHealth; set { _maxHealth = value; } }
        public int CurrentHealth { get => _currentHealth; set { _currentHealth = value; } }
        public int Damage { get => _damage; set { _damage = value; } }
        public float MoveSpeed { get => _moveSpeed; set { _moveSpeed = value; } }
        public HealthBar HealthBar { get => _healthBar; set { _healthBar = value; } }

        private int _currentHealth;
        private Vector2 _moveInput;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _healthBar.SetMaxHealth(_maxHealth);
            GameUpdateManager.Instance.Register(this);
        }

        public void GameUpdate()
        {
            // Рух
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            _moveInput = new Vector2(moveX, moveY).normalized;

            if (_moveInput.sqrMagnitude > 0.01f)
            {
                Vector3 move = new Vector3(_moveInput.x, _moveInput.y, 0f);
                transform.position += move * _moveSpeed * Time.deltaTime;
            }

            // Стрільба
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mouseWorldPos - _firePoint.position);
            direction.z = 0f;

            // Ініціалізація кулі через пул
            Bullet bullet = BulletPool.Instance.GetBullet();
            bullet.transform.position = _firePoint.position;
            bullet.Init(direction, "Enemy", Damage);
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _healthBar.SetHealth(_currentHealth);

            if (_currentHealth <= 0)
            {
                Debug.Log("Player Dead");
                // Можна викликати рестарт гри чи іншу логіку
            }
        }

        private void OnDestroy()
        {
            if (GameUpdateManager.Instance != null)
                GameUpdateManager.Instance.Unregister(this);
        }

    }
}
