using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Scripts
{
    [Serializable]
    public sealed class TabNavigator
    {
        private readonly Stack<GameObject> m_history = new();

        public GameObject Current => m_history.Count > 0 ? m_history.Peek() : null;

        public void Init(GameObject first)
        {
            m_history.Push(first);
        }

        public void Push(GameObject tab)
        {
            if (tab == Current)
            {
                return;
            }

            if (Current)
            {
                Current.SetActive(false);
            }
            
            m_history.Push(tab);
            tab.SetActive(true);
        }

        public void Back()
        {
            if (m_history.Count <= 1)
            {
                return;
            }

            var popped = m_history.Pop();
            
            if (popped)
            {
                popped.SetActive(false);
            }


            if (m_history.Count <= 0)
            {
                return;
            }
            
            var top = m_history.Peek();
            
            if (top)
            {
                top.SetActive(true);
            }
        }
    }
}
