namespace Scenes.Scripts
{
    public interface IOnOffWorkout : IWorkout
    {
        int OnSeconds { get; }
        int OffSeconds { get; }
        int Rounds { get; }
        int TotalSeconds { get; }
    }
}