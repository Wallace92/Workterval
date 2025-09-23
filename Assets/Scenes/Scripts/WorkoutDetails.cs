using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class WorkoutDetails : MonoBehaviour
    {
        [SerializeField]
        private Button m_activationButton;
        [SerializeField]
        private Transform m_workoutContainer;
        
        protected Canvas Canvas;
        
        protected virtual void Awake()
        {
            Canvas = FindFirstObjectByType<Canvas>();
            
            m_activationButton.onClick.AddListener(OnActivationButtonClicked);
        }

        protected virtual void OnDestroy()
        {
            m_activationButton.onClick.RemoveListener(OnActivationButtonClicked);
        }
        
        private void OnActivationButtonClicked()
        {
            m_workoutContainer.gameObject.SetActive(!m_workoutContainer.gameObject.activeSelf);
        }
    }
}