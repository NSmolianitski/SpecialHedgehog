using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialHedgehog.Framework.Services
{
    public class UIService : MonoBehaviour
    {
        private readonly Dictionary<Type, UIScreen> _screensByType = new ();
        private readonly List<UIScreen> _pauseScreens = new ();
        
        private void Awake()
        {
            var screens = GetComponentsInChildren<UIScreen>();
            
            foreach (var screen in screens)
                _screensByType.Add(screen.GetType(), screen);
        }

        public T GetScreen<T>() where T : UIScreen
        {
            return _screensByType[typeof(T)] as T;
        }

        public void AddPauseScreen(UIScreen screen) => _pauseScreens.Add(screen);

        public void ClosePauseScreens()
        {
            foreach (var screen in _pauseScreens)
                screen.Close();
            
            _pauseScreens.Clear();
        }
    }
}