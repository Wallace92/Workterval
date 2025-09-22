using Scenes.Scripts.Models;
using UnityEngine;

namespace Scenes.Scripts.Views
{
    public interface IExerciseTile
    {
        IExercise Exercise { get; }
        GameObject GameObject { get;}
        void Initialize(IExercise exercise, Canvas canvas);
    }
}