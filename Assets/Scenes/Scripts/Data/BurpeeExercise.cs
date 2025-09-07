using Scenes.Scripts.Models.Impl;

namespace Scenes.Scripts.Data
{
    public class BurpeeExercise : Exercise, IBurpeeExercise
    {
        public int Repetitions { get; set; }
        
        public BurpeeExercise(ExerciseDefinition definition) : base(definition)
        {
        }
        
        public override string ToString()
        {
            return $"{Repetitions} Reps";
        }
    }

    public interface IBurpeeExercise
    {
        int Repetitions { get; set; }
    }
}