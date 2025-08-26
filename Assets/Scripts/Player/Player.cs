using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
namespace Assets.Scripts
{
    public class Player : MonoBehaviour, IEntity
    {
        [Header("Stats")]
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _damage = 20;
        [SerializeField] private float _moveSpeed = 5f;


        [Header("Shooting")]
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _fireCooldown = 0.15f;


        private int _currentHealth;
        private Vector2 _moveInput;
        private float _shootTimer;
        private Camera _cam;


        // Input System
        private PlayerInputActions _inputActions;


        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;
        public int Damage => _damage;
        public float MoveSpeed => _moveSpeed;
        public bool IsAlive => _currentHealth > 0;


        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMove;
            _inputActions.Player.Attack.performed += OnShoot;


            GameUpdateManager.Instance.Register(this);
            TargetRegistry.RegisterPlayer(this);
        }

        private void OnDisable()
        {
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Move.canceled -= OnMove;
            _inputActions.Player.Attack.performed -= OnShoot;
            _inputActions.Player.Disable();


            GameUpdateManager.Instance.Unregister(this);
            TargetRegistry.UnregisterPlayer(this);
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            _moveInput = ctx.ReadValue<Vector2>();
        }

        private void OnShoot(InputAction.CallbackContext ctx)
        {
            TryShoot();
        }

        public void GameUpdate(float dt)
        {
            if (!IsAlive) return;

            // Рух
            Vector3 move = new Vector3(_moveInput.x, _moveInput.y, 0f).normalized;
            if (move.sqrMagnitude > 0.001f)
                transform.position += move * (_moveSpeed * dt);


            // Обмеження межами камери
            if (_cam != null)
            {
                Vector3 vp = _cam.WorldToViewportPoint(transform.position);
                vp.x = Mathf.Clamp01(vp.x);
                vp.y = Mathf.Clamp01(vp.y);
                transform.position = _cam.ViewportToWorldPoint(vp);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            }

            // Кулдаун стрільби
            if (_shootTimer > 0f) 
            {
                _shootTimer -= dt;
            }
        }

        private void TryShoot()
        {
            if (_shootTimer > 0f || _firePoint == null) return;
            _shootTimer = _fireCooldown;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth < 0) _currentHealth = 0;
            if (_currentHealth == 0) Die();
        }

        private void Die()
        {
            // Простий рестарт: можна замінити на UI меню
            gameObject.SetActive(false);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}

//Vector3 moveDirection = Vector3.zero;
//if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.up;
//if (Input.GetKey(KeyCode.S)) moveDirection += Vector3.down;
//if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.left;
//if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.right;

//if (moveDirection != Vector3.zero)
//{
//    moveDirection.Normalize();
//    transform.position += moveDirection * this.MoveSpeed * Time.deltaTime;
//}

//float moveX = Input.GetAxisRaw("Horizontal");
//float moveY = Input.GetAxisRaw("Vertical");
//_moveInput = new Vector2(moveX, moveY).normalized;

//if (_moveInput.sqrMagnitude > 0.01f)
//{
//    Vector3 move = new Vector3(_moveInput.x, _moveInput.y, 0f);
//    transform.position += move * _moveSpeed * Time.deltaTime;
//}
