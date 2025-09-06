using Scenes.Scripts.Models.Impl;

namespace Scenes.Scripts.Data
{
    public class BurpeeExercise : Exercise, IBurpeeExercise
    {
        public BurpeeExercise(ExerciseDefinition definition) : base(definition)
        {
        }
    }

    public interface IBurpeeExercise
    {
    }
}