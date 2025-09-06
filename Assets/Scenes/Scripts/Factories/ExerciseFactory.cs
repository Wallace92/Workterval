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
            return def.ExerciseType switch
            {
                ExerciseType.WallBall => new WallBallExercise(def),
                ExerciseType.Row => new RowExercise(def),
                ExerciseType.Burpees => new BurpeeExercise(def),
                _ => new EmptyExercise(def)
            };
        }
    }
}