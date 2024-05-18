
using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;

public class LightHouseStateMachine
{
       private ILightHouseState _currentState;
       private Dictionary<Type, ILightHouseState> _states;

       public void EnterIn<TState>() where TState: ILightHouseState
       {
              if (_states.TryGetValue(typeof(TState),out ILightHouseState state))
              {
                     _currentState?.Exit();
                     _currentState = state;
                     _currentState.Enter();
              }
       }
}
