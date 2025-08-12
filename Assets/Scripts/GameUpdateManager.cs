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

        private readonly List<IUpdatable> _updateables = new();


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Register(IUpdatable updateable)
        {
            if(!_updateables.Contains(updateable))
            {
                _updateables.Add(updateable);
            }
        }

        public void Unregister(IUpdatable updateable)
        {
            if (_updateables.Contains(updateable))
            {
                _updateables.Remove(updateable);
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var updateable in _updateables)
            {
               updateable.GameUpdate();
            }
        }
    }
}