using Scenes.Scripts.Data;
using UnityEngine;

namespace Scenes.Scripts.Models.Impl
{
    public abstract class Exercise : IExercise
    {
        public string Title { get; private set; }
        public Sprite Icon { get; private set;}
        
        protected Exercise(ExerciseDefinition definition)
        {
            Title = definition.Title;
            Icon = definition.Image;
        }
    }
}