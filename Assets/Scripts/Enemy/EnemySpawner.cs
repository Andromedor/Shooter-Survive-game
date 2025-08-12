using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour, IUpdatable
    {
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private GameObject _healthBarPrefab;
        [SerializeField] private Transform _canvasTransform;
        [SerializeField] private float _spawnInterval = 2f;
        private float _timer;


        private void Start()
        {
            GameUpdateManager.Instance.Register(this);
        }

        public void GameUpdate()
        {
            _timer += Time.deltaTime;
            if (_timer >= _spawnInterval)
            {
                _timer = 0f;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            int index = Random.Range(0, _enemyPrefabs.Length);
            Vector2 spawnPos = GetRandomPositionInCamera();
            GameObject enemy = Instantiate(_enemyPrefabs[index], spawnPos, Quaternion.identity);

            HealthBar bar = Instantiate(_healthBarPrefab, _canvasTransform).GetComponent<HealthBar>();
            bar.SetTarget(enemy.transform);

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.HealthBar = bar;
            }
        }

        private Vector2 GetRandomPositionInCamera()
        {
            Camera cam = Camera.main;
          
            Vector2 min = cam.ViewportToWorldPoint(new Vector2(0.05f, 0.05f));
            Vector2 max = cam.ViewportToWorldPoint(new Vector2(0.95f, 0.95f));
            return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
    }
}
