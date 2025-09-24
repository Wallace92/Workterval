using System;
using System.Collections.Generic;
using System.Linq;
using Scenes.Scripts.Data;
using Scenes.Scripts.Factories;
using Scenes.Scripts.Models;
using Scenes.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class WorkoutController : MonoBehaviour
    {
        private readonly List<WorkoutTilePreview> m_workoutPreviews = new();
        
        [Header("Data")]
        [SerializeField] 
        private WorkoutData m_workoutData;
        [SerializeField] 
        private WorkoutControllerView m_workoutControllerView;
        [SerializeField]
        private WorkoutTilePreview m_workoutTilePreviewPrefab;
        [SerializeField]
        private Transform m_workoutTileContainer;
        [SerializeField]
        private OnOffWorkout m_onOffWorkout;
        [SerializeField]
        private Button m_exerciseButton;
        
        private List<IExercise> m_exercises = new();

        private void Awake()
        {
            m_exercises = m_workoutData.Exercises
                .Select(ExerciseFactory.Create)
                .ToList();
            
            m_workoutControllerView.ShowThumbnails(m_exercises);
            
            m_workoutControllerView.WorkoutStarted += OnWorkoutStarted;
            m_exerciseButton.onClick.AddListener(OnExerciseButtonClicked);
        }

        private void OnDestroy()
        {
            m_workoutControllerView.WorkoutStarted -= OnWorkoutStarted;
            
            foreach (var preview in m_workoutPreviews)
            {
                Destroy(preview.gameObject);
            }
            
            m_exerciseButton.onClick.RemoveListener(OnExerciseButtonClicked);
        }

        private void OnWorkoutStarted(IExercise[] exercises, IWorkout workout)
        {
            foreach (var exercise in exercises.Reverse())
            {
                var workoutPreview = Instantiate(m_workoutTilePreviewPrefab, m_workoutTileContainer);
                
                workoutPreview.Initialize(exercise);

                workoutPreview.transform.SetAsFirstSibling();
                
                m_workoutPreviews.Add(workoutPreview);
            }
            
            m_workoutControllerView.SetCurrentTab(m_onOffWorkout.gameObject);
            
            m_onOffWorkout.gameObject.SetActive(true);
            m_onOffWorkout.StartWorkout(workout);
        }

        private void OnExerciseButtonClicked()
        {
            foreach (var workoutPreview in m_workoutPreviews)
            {
                workoutPreview.Toggle();
            }
        }
    }
}