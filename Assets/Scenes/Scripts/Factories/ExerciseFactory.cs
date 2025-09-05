using System;
using Scenes.Scripts.Data;
using Scenes.Scripts.Models;
using Scenes.Scripts.Models.Impl;

namespace Scenes.Scripts.Factories
{
    public class ExerciseFactory
    {
        public static IExercise Create(ExerciseDefinition def)
        {
            if (def == null) throw new ArgumentNullException(nameof(def));

            return def.ExerciseType switch
            {
                ExerciseType.Time => new TimeExercise(def),
                ExerciseType.Repetitions => new RepetitionsExercise(def),
                ExerciseType.Kcal => new EmptyExercise(def),
                ExerciseType.Distance => new EmptyExercise(def),
                _ => throw new NotSupportedException($"Unsupported ExerciseType: {def.ExerciseType}")
            };
        }
    }
}