using Scenes.Scripts.Data;
using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;

namespace Scenes.Scripts.Views
{
    public class RowTileView : ExerciseTileView
    {
        private RowExercise m_exercise;
        
        public override void Initialize(IExercise exercise, Canvas canvas)
        {
            base.Initialize(exercise, canvas);
            
            m_exercise = exercise as RowExercise;
        }
        
        protected override void OnValueConfirmed(int value)
        {
            m_exercise.Kcal = Mathf.Max(1, value);
        }
    }
}