using Scenes.Scripts.Models.Impl;

namespace Scenes.Scripts.Data
{
    public class RowExercise : Exercise, IRowExercise
    {
        public int Kcal { get; set; }
        
        public RowExercise(ExerciseDefinition definition) : base(definition)
        {
        }
        
        public override string ToString()
        {
            return $"{Kcal} Kcal";
        }
    }

    public interface IRowExercise
    {
        int Kcal { get; set; }
    }
}