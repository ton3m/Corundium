namespace CodeBase.Runtime.Core.Actor.SuperStates
{
    public class PlayerInTransportState : IState
    {
        private readonly PlayerMovementStateMachine _stateMachine;

        public PlayerInTransportState(PlayerMovementStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _stateMachine.InputHandler.Input.Gameplay.Disable();
            _stateMachine.InputHandler.Input.Transport.Enable();
        }

        public void Exit()
        {
        }
    }
}