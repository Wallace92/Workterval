using Scenes.Scripts.Data;
using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;

namespace Scenes.Scripts.Views
{
    public class RowTileView : ExerciseTileView
    {
        [SerializeField]
        private WheelPickerOpener m_wheelPickerOpener;
        
        private RowExercise m_exercise;
        
        protected override void Awake()
        {
            base.Awake();

            m_wheelPickerOpener.ValueConfirmed += OnValueConfirmed;
        }
        
        public override void Initialize(IExercise exercise)
        {
            base.Initialize(exercise);
            
            m_exercise = exercise as RowExercise;
        }

        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            m_wheelPickerOpener.ValueConfirmed -= OnValueConfirmed;
        }
        
        private void OnValueConfirmed(int value)
        {
            m_exercise.Kcal = Mathf.Max(1, value);
        }
    }
}