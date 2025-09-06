using System;

namespace Scenes.Scripts.Data
{
    [Flags]
    public enum ExerciseCondition
    {
        Time = 1 << 0,
        Repetitions = 1 << 1,
        Kcal = 1 << 2,
        Distance = 1 << 3,
        Max = 1 << 4,
    }
}