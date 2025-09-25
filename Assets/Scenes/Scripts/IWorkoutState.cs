using System.Collections;

namespace Scenes.Scripts
{
    public interface IWorkoutState
    {
        void Enter();
        void Exit();
        IEnumerator Run();
    }
}