namespace CodeBase.Runtime.Core.Actor.SuperStates
{
    public class PlayerOnFootState : IState
    {
        private readonly PlayerMovementStateMachine _stateMachine;

        public PlayerOnFootState(PlayerMovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _stateMachine.InputHandler.Input.Gameplay.Enable();
            _stateMachine.InputHandler.Input.Transport.Disable();
        }

        public void Exit()
        {
        }
    }
}