using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class GameObjectSelectable : MonoBehaviour
    {
        public event Action FirstButtonClicked = delegate { };
        public event Action SecondButtonClicked = delegate { };
        
        [SerializeField]
        private Button m_firstButton;
        [SerializeField]
        private Button m_secondButton;
        [SerializeField]
        private bool m_isSecondOptionDefault;
        
        protected void Awake()
        {
            m_firstButton.onClick.AddListener(OnFirstButtonClicked);
            m_secondButton.onClick.AddListener(OnSecondButtonClicked);

            if (m_isSecondOptionDefault)
            {
                m_secondButton.image.color = Color.green;
            }
            else
            {
                m_firstButton.image.color = Color.green;
            }
        }
        
        protected void OnDestroy()
        {
            m_firstButton.onClick.RemoveListener(OnFirstButtonClicked);
            m_secondButton.onClick.RemoveListener(OnSecondButtonClicked);
        }
        
        private void OnSecondButtonClicked()
        {
            m_firstButton.image.color = Color.white;
            m_secondButton.image.color = Color.green;
            
            FirstButtonClicked.Invoke();
        }

        private void OnFirstButtonClicked()
        {
            m_firstButton.image.color = Color.green;
            m_secondButton.image.color = Color.white;
            
            SecondButtonClicked.Invoke();
        }
    }
}