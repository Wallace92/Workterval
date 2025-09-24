using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class WorkoutDetails : MonoBehaviour, IWorkout
    {
        public event Action<IWorkout> OnWorkoutSelected = delegate { };
        
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
        
        protected virtual void OnActivationButtonClicked()
        {
            OnWorkoutSelected.Invoke(this);
        }

        public void Show()
        {
            m_workoutContainer.gameObject.SetActive(true);
        }

        public void Hide()
        {
            m_workoutContainer.gameObject.SetActive(false);
        }
    }
}