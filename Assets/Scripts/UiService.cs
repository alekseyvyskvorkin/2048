using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UiService : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 0.5f;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private CanvasGroup _startPanel;

    [SerializeField] private CanvasGroup _losePanel;
    [SerializeField] private Button _restartButton;

    public void FadeStartPanel()
    {
        _startPanel.DOFade(0, _fadeDuration).OnComplete(() => 
        {
            _scoreText.text = "0";
            _scoreText.gameObject.SetActive(true);
        });
    }

    public void UnFadeLosePanel()
    {
        _losePanel.gameObject.SetActive(true);
        _losePanel.DOFade(1, _fadeDuration).OnComplete(() => { _restartButton.interactable = true; });
    }

    public void FadeLosePanel()
    {
        _restartButton.interactable = false;
        _losePanel.DOFade(0, _fadeDuration).OnComplete(() => 
        {
            _scoreText.text = "0";
            _losePanel.gameObject.SetActive(false); 
        });
    }

    public void AddScore(int score)
    {
        _scoreText.text = (int.Parse(_scoreText.text) + score).ToString();
    }
}
