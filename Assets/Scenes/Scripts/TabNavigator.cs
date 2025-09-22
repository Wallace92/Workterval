using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Scripts
{
    [Serializable]
    public sealed class TabNavigator
    {
        private readonly Stack<GameObject> m_history = new();

        public void Init(GameObject first)
        {
            m_history.Push(first);
        }

        public void Push(GameObject tab)
        {
            var current = m_history.Count > 0 
                ? m_history.Peek()
                : null;
            
            if (tab == current)
            {
                return;
            }

            if (current)
            {
                current.SetActive(false);
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
