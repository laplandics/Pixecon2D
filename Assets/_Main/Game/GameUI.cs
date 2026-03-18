using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameUI : MonoBehaviour, ISceneUI
    {
        public Button pauseButton;
        
        private Subject<Unit> _exitSignal;

        public void OnAttached()
        {
            AssignButtons();
        }

        private void AssignButtons()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }

        private void OnPauseButtonClicked()
        {
            _exitSignal?.OnNext(Unit.Default);
        }

        public void OnRemoved() { }

        public void Bind(Subject<Unit> exitSignal)
        {
            _exitSignal = exitSignal;
        }

        public RectTransform UITransform => gameObject.GetComponent<RectTransform>();
    }
}
