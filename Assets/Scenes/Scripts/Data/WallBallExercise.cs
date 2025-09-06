using Scenes.Scripts.Models.Impl;


namespace Scenes.Scripts.Data
{
    public class WallBallExercise : Exercise, IWallBallExercise
    {
        public ExerciseCondition Condition { get; }
        
        public WallBallExercise(ExerciseDefinition definition) : base(definition)
        {
        }
    }

    public interface IWallBallExercise
    {
        ExerciseCondition Condition { get; }
    }
}