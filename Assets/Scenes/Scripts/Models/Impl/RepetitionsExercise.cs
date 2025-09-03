using Scenes.Scripts.Data;
using UnityEngine;

namespace Scenes.Scripts.Models.Impl
{
    public class RepetitionsExercise : IExercise
    {
        public string Title { get; private set; }
        public Sprite Icon { get; private set;}
        public string Description { get;  private set;}

        public int DurationInSeconds { get; private set; }
        public float Progress { get; private set;}
        public bool IsCompleted { get; private set;}

        public void Initialize(ExerciseDefinition definition, float durationInSeconds)
        {
            Title = definition.Title;
            Icon = definition.Image;
            DurationInSeconds = (int)durationInSeconds;
            Progress = 0f;
            IsCompleted = false;
        }
        
        public void OnPrimaryAction()
        {
            //
        }

        public void Reset()
        {
            //
        }
    }
}