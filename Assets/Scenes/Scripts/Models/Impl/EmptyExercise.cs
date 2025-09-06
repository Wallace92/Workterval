using Scenes.Scripts.Data;
using UnityEngine;

namespace Scenes.Scripts.Models.Impl
{
    public class EmptyExercise : IExercise
    {
        public string Title { get; private set; }
        public Sprite Icon { get; private set;} = null;
        public string Description { get;  private set; }

        public float Progress { get; private set;} = 0f;
        public bool IsCompleted { get; private set;} = true;
        
        public EmptyExercise(ExerciseDefinition definition)
        {
            Title = definition.Title;
            Icon = definition.Image;
            
            Debug.Log($"EmptyExercise created: {Title}");
        }

        public void Initialize(ExerciseDefinition definition, float param)
        {
            //
        }

        public void OnPrimaryAction()
        {
            //
        }

        public void Reset()
        {
            //
        }

        public void Tick(float deltaTime)
        {
            //
        }
    }
}