using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace _Game.Gameplay.Logic.UI
{
    public class LoseView : MonoBehaviour
    {
        public TextMeshProUGUI PointLabel;
        public Button RestartButton;
        public Button QuitButton;

        [SerializeField] private RectTransform _losePanel;
        [SerializeField] private float _durationAnimationShow;

        private Vector3 _losePanelDefaultLocalScale;

        private void Start()
        {
            _losePanelDefaultLocalScale = _losePanel.localScale;
            Hide();
        }

        public void ShowPoints(string points)
        {
            PointLabel.text = points;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _losePanel.DOScale(_losePanelDefaultLocalScale, _durationAnimationShow).SetEase(Ease.OutBounce);
        }

        private void Hide()
        {
            _losePanel.localScale = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}