using System;
using System.Collections.Generic;
using StatefulUISupport.Scripts.Components;
using UnityEngine;

namespace SpecialHedgehog.Framework.Services
{
    public class UIService : MonoBehaviour
    {
        private readonly Dictionary<Type, StatefulView> _screensByType = new ();
        
        private void Awake()
        {
            var screens = GetComponentsInChildren<StatefulView>();
            foreach (var screen in screens)
            {
                _screensByType.Add(screen.GetType(), screen);
            }
        }

        public T GetScreen<T>() where T : StatefulView
        {
            return _screensByType[typeof(T)] as T;
        }
    }
}