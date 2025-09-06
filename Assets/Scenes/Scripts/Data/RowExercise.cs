using Scenes.Scripts.Models.Impl;

namespace Scenes.Scripts.Data
{
    public class RowExercise : Exercise, IRowExercise
    {
        public int Kcal { get; set; }
        
        public RowExercise(ExerciseDefinition definition) : base(definition)
        {
        }
    }

    public interface IRowExercise
    {
        int Kcal { get; set; }
    }
}