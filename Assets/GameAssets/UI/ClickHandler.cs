using System;
using UnityEngine;
using UnityEngine.UI;

namespace CrazyPawns.GameAssets.UI
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private Action _deactivationCallback;

        public bool IsActive
        {
            get => _button.gameObject.activeSelf;
            private set => _button.gameObject.SetActive(value);
        }

        private void Start()
        {
            DeactivateButton();
            _button.onClick.AddListener(DeactivateButton);
        }

        public void ActivateButton(Action callback)
        {
            IsActive = true;
            _deactivationCallback = callback;
        }

        private void DeactivateButton()
        {
            IsActive = false;
            _deactivationCallback?.Invoke();
        }

        private void OnDestroy() => _button.onClick?.RemoveAllListeners();
    }
}
