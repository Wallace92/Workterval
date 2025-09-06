using Scenes.Scripts.Data;
using Scenes.Scripts.Models;
using Scenes.Scripts.Views;
using UnityEngine;

namespace Scenes.Scripts.Factories
{
    public class ExerciseTileViewFactory : MonoBehaviour
    {
        [SerializeField]
        private WallBallTileView m_wallBallTilePrefab;
        [SerializeField]
        private RowTileView m_rowTilePrefab;
        [SerializeField]
        private BurpeeTileView m_burpeePrefab;
        [SerializeField]
        private EmptyExerciseTile m_emptyExerciseTile;
        
        public IExerciseTile Create(IExercise exercise, Transform container)
        {
            IExerciseTile tile = exercise switch
            {
                IWallBallExercise => Instantiate(m_wallBallTilePrefab, container),
                IRowExercise => Instantiate(m_rowTilePrefab, container),
                IBurpeeExercise => Instantiate(m_burpeePrefab, container),
                _ => Instantiate(m_emptyExerciseTile, container)
            };

            tile.Initialize(exercise);
            
            return tile;
        }
    }
}