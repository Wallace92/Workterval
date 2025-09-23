using System;
using System.Collections.Generic;
using Scenes.Scripts.Factories;
using Scenes.Scripts.Models;
using Scenes.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Scripts
{
    public class WorkoutControllerView : MonoBehaviour
    {
        public event Action<IExercise[], IOnOffWorkout> WorkoutStarted = delegate { };
        
        private readonly List<ThumbnailTileView> m_thumbnailTileViews = new();
        private readonly List<IExerciseTile> m_exerciseTileViews = new();
        private readonly List<IExercise> m_selectedExercises = new();
        private readonly TabNavigator m_tabNavigator = new();
        
        [SerializeField]
        private Canvas m_canvas;
        
        [Header("Thumbnail View")]
        [SerializeField] 
        private Transform m_thumbnailContainer;     
        [SerializeField] 
        private ThumbnailTileView m_thumbnailTilePrefab;
        [SerializeField]
        private GameObject m_thumbnailTilesParent;
       
        [Header("Exercise View")]
        [SerializeField] 
        private Transform m_exerciseContainer; 
        [SerializeField]
        private GameObject m_exerciseTilesParent;
      
        [Header("Controls")]
        [SerializeField] 
        private Button m_workoutButton;
        [SerializeField] 
        private Button m_backButton;
        [SerializeField] 
        private Button m_startButton;
        [SerializeField]
        private ExerciseTileViewFactory m_exerciseTileViewFactory;
        [SerializeField]
        private OnOffWorkoutDetails m_workoutDetails;

        private void Awake()
        {
            m_startButton.onClick.AddListener(OnStartButton);
            m_workoutButton.onClick.AddListener(OnWorkoutButton);
            m_backButton.onClick.AddListener(OnBackButton);
            
            m_tabNavigator.Init(m_thumbnailTilesParent);
        }

        private void OnDestroy()
        {
            foreach (var tile in m_thumbnailTileViews)
            {
                tile.ThumbnailClicked -= HandleThumbnailClicked;
            }
            
            m_startButton.onClick.RemoveListener(OnStartButton);
            m_workoutButton.onClick.RemoveListener(OnWorkoutButton);
            m_backButton.onClick.RemoveListener(OnBackButton);
        }

        public void ShowThumbnails(List<IExercise> exercises)
        {
            foreach (var exercise in exercises)
            {
                var tile = Instantiate(m_thumbnailTilePrefab, m_thumbnailContainer);
                
                tile.Initialize(exercise);
                
                tile.ThumbnailClicked += HandleThumbnailClicked;
                
                m_thumbnailTileViews.Add(tile);
            }
        }

        public void SetCurrentTab(GameObject currentTab)
        {
            m_tabNavigator.Push(currentTab);
        }

        private void OnBackButton()
        {
            m_tabNavigator.Back();
        }

        private void OnStartButton()
        {
            WorkoutStarted?.Invoke(m_selectedExercises.ToArray(), m_workoutDetails);
        }
        
        private void OnWorkoutButton()
        {
            m_backButton.gameObject.SetActive(true);
            
            m_tabNavigator.Push(m_exerciseTilesParent);
        }
        
        private void HandleThumbnailClicked(ThumbnailTileView tile)
        {
            OnTileClicked(tile);
        }
        
        private void OnTileClicked(ThumbnailTileView tile)
        {
            var exercise = tile.Exercise;
            
            if (m_selectedExercises.Contains(exercise))
            {
                tile.Display(false);
                RemoveExerciseTile(exercise);
                m_selectedExercises.Remove(exercise);
                return;
            }

            tile.Display(true);
            InstantiateExerciseTile(exercise);
            
            m_selectedExercises.Add(exercise);
        }

        private void InstantiateExerciseTile(IExercise exercise)
        {
            var tile = m_exerciseTileViewFactory.Create(exercise, m_canvas, m_exerciseContainer);
            
            m_exerciseTileViews.Add(tile);
        }
        
        private void RemoveExerciseTile(IExercise exercise)
        {
            var tile = m_exerciseTileViews.Find(t => t.Exercise == exercise);
            if (tile == null)
            {
                return;
            }
            
            m_exerciseTileViews.Remove(tile);
            
            Destroy(tile.GameObject);
        }
    }
}