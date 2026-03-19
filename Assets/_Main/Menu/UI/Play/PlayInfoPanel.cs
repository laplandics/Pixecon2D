using System.Collections.Generic;
using Proxy;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class PlayInfoPanel : MonoBehaviour, IMenuUiInfoPanel
    {
        public Button playButton;
        
        private Subject<Unit> _playSignal;

        public void LoadElements(VocabularyCreator vocabCreator, Dictionary<string, Subject<Unit>> signals)
        {
            _playSignal = signals[MenuUiInteractor.PLAY_BUTTON_SIGNAL_NAME];
            playButton.onClick.AddListener(() => _playSignal.OnNext(Unit.Default));
        }

        public void ClearElements()
        {
            Destroy(gameObject);
        }
    }
}