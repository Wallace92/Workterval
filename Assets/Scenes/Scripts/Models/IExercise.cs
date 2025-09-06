using Scenes.Scripts.Data;
using UnityEngine;

namespace Scenes.Scripts.Models
{
    public interface IExercise
    {
        string Title { get; }
        Sprite Icon { get; }
    }
}