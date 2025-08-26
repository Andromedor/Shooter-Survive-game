using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HealthBar : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Slider _slider; // очікуємо 0..MaxHealth (int)
        [SerializeField] private Vector3 _worldOffset = new Vector3(0, 1.1f, 0);

        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
            GameUpdateManager.Instance.Register(this);
        }

        public void SetMaxHealth(int max)
        {
            if (_slider != null)
            {
                _slider.minValue = 0;
                _slider.maxValue = max;
            }
        }

        public void SetHealth(int value)
        {
            if (_slider != null) _slider.value = value;
        }

        public void GameUpdate(float dt)
        {
            if (_target == null) return;
            var cam = Camera.main;
            if (cam == null) return;
            transform.position = cam.WorldToScreenPoint(_target.position + _worldOffset);
        }

        private void OnDisable()
        {
            if (GameUpdateManager.Instance != null)
                GameUpdateManager.Instance.Unregister(this);
        }
    }
}
