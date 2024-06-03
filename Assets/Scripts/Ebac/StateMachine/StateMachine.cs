using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


namespace Ebac.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {

        public Dictionary<T, StateBase> dictionaryState;
        private StateBase _currentState;

        public float TimeToStartGame = 1f;

        public StateBase CurrentState
        {
            get { return _currentState; }
        }


        public void Init()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            dictionaryState.Add(typeEnum, state);
        }


#if UNITY_EDITOR //esse unity_editor, garante que so fique no DEBUG, quando for compilar ele apagara essa linha de codigo.
        #region DEBUG
        /*[Button]
        private void ChangeStateToStateX()
        {
            SwitchState(States.NONE);
        }

        [Button]
        private void ChangeStateToStateY()
        {
            SwitchState(States.NONE);
        }*/

        #endregion
#endif

        public void SwitchState(T state, params object[] objs)
        {
            if (_currentState != null) _currentState.onStateExit();

            _currentState = dictionaryState[state];

            _currentState.onStateEnter(objs);
        }

        public void Update()
        {
            if (_currentState != null) _currentState.onStateStay();
        }
    }
}