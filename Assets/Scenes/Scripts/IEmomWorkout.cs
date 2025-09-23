namespace Scenes.Scripts
{
    public interface IEmomWorkout : IWorkout
    {
        int Rounds { get; }
        int RoundSeconds { get; }
    }
}