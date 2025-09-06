using Scenes.Scripts.Data;
using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;

namespace Scenes.Scripts.Views
{
    public class RowTileView : ExerciseTileView
    {
        [SerializeField]
        private TMP_InputField m_kcalInputField;
        
        private RowExercise m_exercise;
        
        protected override void Awake()
        {
            base.Awake();

            m_kcalInputField.onEndEdit.AddListener(OnKcalEdited);
        }
        
        public override void Initialize(IExercise exercise)
        {
            base.Initialize(exercise);
            
            m_exercise = exercise as RowExercise;
        }

        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_kcalInputField.onEndEdit.RemoveListener(OnKcalEdited);
        }
        
        private void OnKcalEdited(string _)
        {
            if (int.TryParse(m_kcalInputField.text, out var reps))
            {
                m_exercise.Kcal = Mathf.Max(1, reps);
            }
        }
    }
}