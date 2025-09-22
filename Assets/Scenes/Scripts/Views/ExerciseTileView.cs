using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts.Views
{
    public class ExerciseTileView : MonoBehaviour, IExerciseTile
    {
        [Header("UI")]
        [SerializeField]
        private Image icon;
        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private WheelPickerOpener m_wheelPickerOpener;
        
        private IExercise m_exercise;
        private Canvas m_canvas;
        
        public IExercise Exercise => m_exercise;
        public GameObject GameObject => gameObject;

        protected virtual void Awake()
        {
            m_wheelPickerOpener.ValueConfirmed += OnValueConfirmed;
            m_wheelPickerOpener.WheelOpened += OnWheelOpened;
        }

        protected virtual void OnDestroy()
        {
            m_wheelPickerOpener.ValueConfirmed -= OnValueConfirmed;
            m_wheelPickerOpener.WheelOpened -= OnWheelOpened;
        }

        protected virtual void OnValueConfirmed(int value)
        {
            
        }
        
        private void OnWheelOpened()
        {
            m_wheelPickerOpener.Open(m_canvas);
        }

        public virtual void Initialize(IExercise exercise, Canvas canvas)
        {
            m_exercise = exercise;
            m_canvas = canvas;
            
            icon.sprite = m_exercise.Icon;
            title.text = m_exercise.Title;
        }
    }
}