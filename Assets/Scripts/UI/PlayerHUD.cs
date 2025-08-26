using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerHUD : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Slider _healthSlider; // 0..1
        [SerializeField] private Text _scoreText; // опціонально

        private bool _initialized;

        private void OnEnable()
        {
            GameUpdateManager.Instance.Register(this);
        }

        private void OnDisable()
        {
            GameUpdateManager.Instance.Unregister(this);
        }

        public void GameUpdate(float dt)
        {
            var player = TargetRegistry.Player as IEntity;
            if (player == null) return;


            if (!_initialized)
            {
                _healthSlider.minValue = 0f;
                _healthSlider.maxValue = 1f;
                _initialized = true;
            }


            float normalized = player.MaxHealth > 0 ? (float)player.CurrentHealth / player.MaxHealth : 0f;
            _healthSlider.value = normalized;
        }

        public void SetScore(int score)
        {
            if (_scoreText != null) _scoreText.text = $"Score: {score}";
        }
    }
}

