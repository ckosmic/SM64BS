using SM64BS.Behaviours;
using SM64BS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS.Managers
{
    internal class GameEventManager : MonoBehaviour
    {
        private EventManager _eventManager;

        private ScoreController _scoreController;
        private GameEnergyCounter _gameEnergyCounter;

        public void Initialize(EventManager eventManager, EventObjects eventObjects)
        {
            _eventManager = eventManager;

            _scoreController = eventObjects.scoreController;
            _gameEnergyCounter = eventObjects.gameEnergyCounter;

            SubscribeToEvents();

            _eventManager.OnInitialize?.Invoke();
        }

        private void OnDisable()
        {
            _eventManager.OnDispose?.Invoke();

            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _scoreController.noteWasCutEvent += NoteWasCutHandler;
            _scoreController.noteWasMissedEvent += NoteMissedHandler;

            _gameEnergyCounter.gameEnergyDidReach0Event += GameEnergyDidReach0Handler;
        }

        private void UnsubscribeFromEvents()
        {
            _scoreController.noteWasCutEvent -= NoteWasCutHandler;
            _scoreController.noteWasMissedEvent -= NoteMissedHandler;
        }

        private void NoteWasCutHandler(NoteData a1, in NoteCutInfo a2, int a3)
        {
            _eventManager.OnNoteCut?.Invoke();
        }

        private void NoteMissedHandler(NoteData a1, int a2)
        {
            _eventManager.OnNoteMissed?.Invoke();
        }

        private void GameEnergyDidReach0Handler()
        {
            _eventManager.OnEnergyDidReach0?.Invoke();
        }
    }
}
