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
        private WheelPickerOpener m_wheelPickerOpener;
        [SerializeField]
        private GameObjectSelectable m_objectSelectable;
        
        private WallBallExercise m_exercise;
        
        protected override void Awake()
        {
            base.Awake();
            
            m_objectSelectable.FirstButtonClicked += OnRepsButtonClicked;
            m_objectSelectable.SecondButtonClicked += OnMaxButtonClicked;
            
            m_wheelPickerOpener.ValueConfirmed += OnValueConfirmed;
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
            
            m_wheelPickerOpener.ValueConfirmed -= OnValueConfirmed;
        }

        private void OnValueConfirmed(int value)
        {
            m_exercise.Repetitions = Mathf.Max(1, value);
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