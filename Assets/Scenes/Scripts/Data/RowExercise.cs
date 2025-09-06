using Scenes.Scripts.Models.Impl;

namespace Scenes.Scripts.Data
{
    public class RowExercise : Exercise, IRowExercise
    {
        public RowExercise(ExerciseDefinition definition) : base(definition)
        {
        }
    }

    public interface IRowExercise
    {
    }
}