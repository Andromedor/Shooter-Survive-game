using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameUpdateManager : MonoBehaviour
    {
        private static GameUpdateManager _instance;
        public static GameUpdateManager Instance => _instance;

        private static readonly List<IUpdatable> _updatables = new();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this; 
            }
            else
                Destroy(gameObject);
        }

        public void Register(IUpdatable updatable)
        {
            if (!_updatables.Contains(updatable))
            {
                _updatables.Add(updatable);
            }         
        }

        public void Unregister(IUpdatable updatable)
        {
            if (_updatables.Contains(updatable))
            {
                _updatables.Remove(updatable);
            }
        }

        // Update is called once per frame
        void Update()
        {
            float dt = Time.deltaTime;

            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].GameUpdate(dt);
            }     
        }
    }
}