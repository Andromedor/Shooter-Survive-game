using Assets.Scripts;
using UnityEngine;
namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour, IUpdatable
    {
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _speed = 10f;
 
        private Vector2 _direction;
        private string _targetTag;
        private bool _initialized;

        public void Init(Vector2 direction, string targetTag, int damage)
        {
            _direction = direction.normalized;
            _targetTag = targetTag;
            _damage = damage;
            _initialized = true;
            GameUpdateManager.Instance.Register(this);
        }

        public void GameUpdate()
        {
            if (!_initialized) return;
            transform.position += (Vector3)_direction * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(_targetTag))
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                    damageable.TakeDamage(_damage);

                Deactivate();
            }
        }

        private void Deactivate()
        {
            _initialized = false;
            GameUpdateManager.Instance.Unregister(this);
            BulletPool.Instance.ReturnBullet(this);
        }
    }
}
