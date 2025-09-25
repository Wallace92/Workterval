namespace Scenes.Scripts.Data.WorkoutStates
{
    public class CompletedState : IWorkoutState
    {
        private readonly Workout m_ctx;

        public CompletedState(Workout ctx)
        {
            m_ctx = ctx;
        }

        public void Enter()
        {
            m_ctx.UI_Completed();
        }

        public void Exit()
        {
            
        }

        public System.Collections.IEnumerator Run()
        {
            yield break;
        }
    }
}