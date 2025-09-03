using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Scripts.Data
{
    [CreateAssetMenu(menuName = "Workout/Workout Data")]
    public class WorkoutData : ScriptableObject
    {
        public List<ExerciseDefinition> Exercises = new();
    }
}