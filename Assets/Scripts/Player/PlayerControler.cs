using UnityEngine;

namespace Assets.Scripts
{
    //public class PlayerControler : MonoBehaviour, IUpdatable, IDamageable
    //{
    //    [SerializeField] private float _moveSpeed = 5f;
    //    [SerializeField] private int _maxHealth = 100;
    //    [SerializeField] private Transform _firePoint;

    //    private Vector2 _moveInput;
    //    private int _currentHealth;

    //    private void Start()
    //    {
    //        _currentHealth = _maxHealth;
    //        GameUpdateManager.Instance.Register(this);
    //    }

    //    public void GameUpdate()
    //    {
    //        float moveX = Input.GetAxisRaw("Horizontal");
    //        float moveY = Input.GetAxisRaw("Vertical");
    //        _moveInput = new Vector2(moveX, moveY).normalized;

    //        if (_moveInput.sqrMagnitude > 0.01f)
    //        {
    //            Vector3 move = new Vector3(_moveInput.x, _moveInput.y, 0f);
    //            transform.position += move * _moveSpeed * Time.deltaTime;
    //        }
    //        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            Shoot();
    //        }
    //    }

    //    private void Shoot()
    //    {
    //        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Vector3 direction = (mouseWorldPos - _firePoint.position);
    //        direction.z = 0f;

    //        Bullet bullet = BulletPool.Instance.GetBullet();
    //        bullet.transform.position = _firePoint.position;
    //        bullet.Init(direction, "Enemy");
    //    }

    //    public void TakeDamage(int amount)
    //    {
    //        _currentHealth -= amount;
    //        Debug.Log("Player HP: " + _currentHealth);

    //        if (_currentHealth <= 0)
    //        {
    //            // Game over / restart
    //        }
    //    }
    //}
}
