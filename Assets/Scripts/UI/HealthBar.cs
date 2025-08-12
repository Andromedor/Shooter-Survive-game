using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HealthBar : MonoBehaviour, IUpdatable
    {

        [SerializeField] private Slider _slider;
        private Transform _target;
        private Vector3 _offset = new Vector3(0, 1.2f, 0);

        public Action ActionDestroy;

        public void SetTarget(Transform target)
        {
            _target = target;
            GameUpdateManager.Instance.Register(this);
            ActionDestroy += Destroy;
        }

        public void SetMaxHealth(int maxHealth)
        {
            _slider.maxValue = maxHealth;
            _slider.value = maxHealth;
        }

        public void SetHealth(int currentHealth)
        {
            _slider.value = currentHealth;
        }

        public void GameUpdate()
        {
            if (_target != null)
                transform.position = Camera.main.WorldToScreenPoint(_target.position + _offset);
        }

        private void Destroy()
        {
            ActionDestroy -= Destroy;
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (GameUpdateManager.Instance != null)
                GameUpdateManager.Instance.Unregister(this);
        }
    }
}
