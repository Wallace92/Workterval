using Scenes.Scripts.Data;
using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;


namespace Scenes.Scripts.Views
{
    public class WallBallTileView : ExerciseTileView
    {
        [SerializeField]
        private TMP_InputField m_repsInputField;
        [SerializeField]
        private GameObjectSelectable m_objectSelectable;
        
        private WallBallExercise m_exercise;
        
        protected override void Awake()
        {
            base.Awake();
            
            m_objectSelectable.FirstButtonClicked += OnRepsButtonClicked;
            m_objectSelectable.SecondButtonClicked += OnMaxButtonClicked;
            
            m_repsInputField.onEndEdit.AddListener(OnRepsEdited);
        }
        
        public override void Initialize(IExercise exercise)
        {
            base.Initialize(exercise);
            
            m_exercise = exercise as WallBallExercise;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_objectSelectable.FirstButtonClicked -= OnRepsButtonClicked;
            m_objectSelectable.SecondButtonClicked -= OnMaxButtonClicked;
            
            m_repsInputField.onEndEdit.RemoveListener(OnRepsEdited);
        }

        private void OnRepsEdited(string _)
        {
            if (int.TryParse(m_repsInputField.text, out var reps))
            {
                m_exercise.Repetitions = Mathf.Max(1, reps);
            }
        }

        private void OnMaxButtonClicked()
        {
            m_exercise.Condition = ExerciseCondition.Max;
            
            m_repsInputField.gameObject.SetActive(false);
        }

        private void OnRepsButtonClicked()
        {
            m_exercise.Condition = ExerciseCondition.Max;
            
            m_repsInputField.gameObject.SetActive(true);
        }
    }
}