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
        
        private IExercise m_exercise;
        
        public IExercise Exercise => m_exercise;
        public GameObject GameObject => gameObject;

        protected virtual void Awake()
        {
            
        }

        protected virtual void OnDestroy()
        {
            
        }

        public virtual void Initialize(IExercise exercise)
        {
            m_exercise = exercise;
            
            icon.sprite = m_exercise.Icon;
            title.text = m_exercise.Title;
        }
    }
}