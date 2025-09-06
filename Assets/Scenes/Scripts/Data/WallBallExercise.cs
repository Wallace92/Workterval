using Scenes.Scripts.Models.Impl;


namespace Scenes.Scripts.Data
{
    public class WallBallExercise : Exercise, IWallBallExercise
    {
        public ExerciseCondition Condition { get; set; }
        public int Repetitions { get; set; }
        
        public WallBallExercise(ExerciseDefinition definition) : base(definition)
        {
        }
    }

    public interface IWallBallExercise
    {
        ExerciseCondition Condition { get; }
        int Repetitions { get; set; }
    }
}