using Scenes.Scripts.Data;
using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;

namespace Scenes.Scripts.Views
{
    public class BurpeeTileView : ExerciseTileView
    {
        [SerializeField]
        private TMP_InputField m_repsInputField;
        
        private BurpeeExercise m_exercise;
        
        protected override void Awake()
        {
            base.Awake();

            m_repsInputField.onEndEdit.AddListener(OnRepsEdited);
        }
        
        public override void Initialize(IExercise exercise)
        {
            base.Initialize(exercise);
            
            m_exercise = exercise as BurpeeExercise;
        }

        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_repsInputField.onEndEdit.RemoveListener(OnRepsEdited);
        }
        
        private void OnRepsEdited(string _)
        {
            if (int.TryParse(m_repsInputField.text, out var reps))
            {
                m_exercise.Repetitions = Mathf.Max(1, reps);
            }
        }
    }
}