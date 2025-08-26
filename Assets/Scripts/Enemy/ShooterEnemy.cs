using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class ShooterEnemy : Enemy
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _fireRate = 1.5f;
        private float _fireTimer;


        private Vector3 _roamTarget;


        protected override void Start()
        {
            base.Start();
            PickRoamTarget();
        }


        public override void GameUpdate(float dt)
        {
            if (!IsAlive) return;


            // Рух до випадкової точки в межах камери
            transform.position = Vector3.MoveTowards(transform.position, _roamTarget, _moveSpeed * dt);
            if (Vector3.Distance(transform.position, _roamTarget) < 0.2f)
                PickRoamTarget();


            // Стрільба по гравцю
            _fireTimer -= dt;
            if (_fireTimer <= 0f && TargetRegistry.Player != null && _firePoint != null)
            {
                _fireTimer = _fireRate;
                Vector3 dir = (((MonoBehaviour)TargetRegistry.Player).transform.position - _firePoint.position).normalized;
                BulletPool.Instance.Spawn(_firePoint.position, dir, false, _damage);
            }
        }

        private void PickRoamTarget()
        {
            if (_cam == null) _cam = Camera.main;
            Vector3 min = _cam.ViewportToWorldPoint(new Vector3(0.08f, 0.08f, 0f));
            Vector3 max = _cam.ViewportToWorldPoint(new Vector3(0.92f, 0.92f, 0f));
            _roamTarget = new Vector3(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y),
            0f
            );
        }
    }
}
