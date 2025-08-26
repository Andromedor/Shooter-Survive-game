using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Enemy[] _enemyPrefabs;
        [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private Transform _uiCanvas; // Screen Space - Overlay
        [SerializeField] private float _spawnInterval = 2.5f;

        private float _timer;
        private Camera _cam;

        private void Start()
        {
            _cam = Camera.main;
            GameUpdateManager.Instance.Register(this);
        }

        public void GameUpdate(float dt)
        {
            _timer += dt;
            if (_timer >= _spawnInterval)
            {
                _timer = 0f;
                SpawnOne();
            }
        }

        private void SpawnOne()
        {
            if (_enemyPrefabs == null || _enemyPrefabs.Length == 0) return;
            var prefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
            Vector3 pos = RandomPointInsideCamera(0.85f);
            Enemy enemy = Instantiate(prefab, pos, Quaternion.identity);


            if (_healthBarPrefab != null && _uiCanvas != null)
            {
                HealthBar hb = Instantiate(_healthBarPrefab, _uiCanvas);
                enemy.SetHealthBar(hb);
            }
        }

        private Vector3 RandomPointInsideCamera(float margin = 0.9f)
        {
            float m = Mathf.Clamp01(margin);
            Vector3 min = _cam.ViewportToWorldPoint(new Vector3(1f - m, 1f - m, 0f));
            Vector3 max = _cam.ViewportToWorldPoint(new Vector3(m, m, 0f));
            // виправлення: краще явно з 0..1 з відступом
            Vector3 p = _cam.ViewportToWorldPoint(new Vector3(Random.Range(0.05f, 0.95f), Random.Range(0.05f, 0.95f), 0f));
            p.z = 0f;
            return p;
        }
    }
}
