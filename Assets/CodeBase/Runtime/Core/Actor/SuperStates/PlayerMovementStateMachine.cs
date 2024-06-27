using Zenject;

namespace CodeBase.Runtime.Core.Actor.SuperStates
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public IInputHandler InputHandler { get; private set; }
        
        [Inject]
        public void Construct(IInputHandler inputHandler)
        {
            InputHandler = inputHandler;
        }
        
        public PlayerMovementStateMachine()
        {
            RegisterState(new PlayerOnFootState(this));
            RegisterState(new PlayerInTransportState(this));
        }
    }
}