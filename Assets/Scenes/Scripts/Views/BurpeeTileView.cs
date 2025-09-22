using Scenes.Scripts.Data;
using Scenes.Scripts.Models;
using TMPro;
using UnityEngine;

namespace Scenes.Scripts.Views
{
    public class BurpeeTileView : ExerciseTileView
    {
        private BurpeeExercise m_exercise;
        
        public override void Initialize(IExercise exercise, Canvas canvas)
        {
            base.Initialize(exercise, canvas);
            
            m_exercise = exercise as BurpeeExercise;
        }

        protected override void OnValueConfirmed(int value)
        {
            m_exercise.Repetitions = Mathf.Max(1, value);
        }

    }
}