using Scenes.Scripts.Data;
using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Scenes.Scripts.Views
{
    public class WallBallTileView : ExerciseTileView
    {
        [SerializeField]
        private Button m_maxButton;
        [SerializeField]
        private Button m_repsButton;
        [SerializeField]
        private TMP_InputField m_repsInputField;
        
        private WallBallExercise m_exercise;
        
        protected override void Awake()
        {
            base.Awake();
            
            m_repsButton.onClick.AddListener(OnRepsButtonClicked);
            m_maxButton.onClick.AddListener(OnMaxButtonClicked);
            
            m_repsInputField.onEndEdit.AddListener(OnRepsEdited);
            
            m_repsButton.image.color = Color.green;
        }
        
        public override void Initialize(IExercise exercise)
        {
            base.Initialize(exercise);
            
            m_exercise = exercise as WallBallExercise;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_repsButton.onClick.RemoveListener(OnRepsButtonClicked);
            m_maxButton.onClick.RemoveListener(OnMaxButtonClicked);
            
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
            
            m_repsButton.image.color = Color.white;
            m_maxButton.image.color = Color.green;
            
            m_repsInputField.gameObject.SetActive(false);
        }

        private void OnRepsButtonClicked()
        {
            m_exercise.Condition = ExerciseCondition.Max;
            
            m_maxButton.image.color = Color.white;
            m_repsButton.image.color = Color.green;
            
            m_repsInputField.gameObject.SetActive(true);
        }
    }
}