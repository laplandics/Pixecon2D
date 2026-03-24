using UnityEngine;
using UnityEngine.UI;

namespace GameView
{
    public class NoVocabulariesPopupBinder : MonoBehaviour, IPopup
    {
        public Button menuButton;
        public Button replayButton;
        
        private PopupViewModel _vm;
        
        public void Bind(PopupViewModel vm)
        {
            _vm = vm;
        }
        
        public void OnShow()
        {
            AttachButtons();
        }

        private void AttachButtons()
        {
            menuButton.onClick.AddListener(OnMenuButtonClick);
            replayButton.onClick.AddListener(OnReplayButtonClick);
        }

        private void OnMenuButtonClick()
        {
            _vm.CycleHandler.FinishGame();
        }

        private void OnReplayButtonClick()
        {
            _vm.CycleHandler.ReplayGame();
        }
        
        public void OnHide() { }
        
        public RectTransform PopupTransform => gameObject.GetComponent<RectTransform>();
    }
}