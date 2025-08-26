using Assets.Scripts;
using UnityEngine;
namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour, IUpdatable
    {
        [SerializeField] private float _speed = 12f;
        [SerializeField] private float _hitRadius = 0.28f;
        [SerializeField] private float _maxLifetime = 5f;


        private Vector3 _dir;
        private bool _fromPlayer;
        private int _damage;
        private float _life;


        public void Activate(Vector3 dir, bool fromPlayer, int damage)
        {
            _dir = dir.normalized;
            _fromPlayer = fromPlayer;
            _damage = damage;
            _life = 0f;
            GameUpdateManager.Instance.Register(this);
            gameObject.SetActive(true);
        }


        public void GameUpdate(float dt)
        {
            transform.position += _dir * (_speed * dt);
            _life += dt;


            if (_life >= _maxLifetime || !IsVisibleOnScreen())
            {
                BulletPool.Instance.Recycle(this);
                return;
            }


            if (_fromPlayer)
            {
                var enemies = TargetRegistry.GetEnemies();
                for (int i = 0; i < enemies.Count; i++)
                {
                    var e = enemies[i];
                    if (!e.IsAlive) continue;
                    Vector3 etf = ((MonoBehaviour)e).transform.position;
                    if (Vector3.Distance(transform.position, etf) <= _hitRadius)
                    {
                        e.TakeDamage(_damage);
                        BulletPool.Instance.Recycle(this);
                        return;
                    }
                }
            }
            else
            {
                var p = TargetRegistry.Player;
                if (p != null)
                {
                    Vector3 ptf = ((MonoBehaviour)p).transform.position;
                    if (Vector3.Distance(transform.position, ptf) <= _hitRadius)
                    {
                        p.TakeDamage(_damage);
                        BulletPool.Instance.Recycle(this);
                        return;
                    }
                }
            }
        }


        private bool IsVisibleOnScreen()
        {
            var cam = Camera.main;
            if (cam == null) return true;
            Vector3 vp = cam.WorldToViewportPoint(transform.position);
            return vp.x >= -0.1f && vp.x <= 1.1f && vp.y >= -0.1f && vp.y <= 1.1f; // трохи запасу
        }
    }
}
