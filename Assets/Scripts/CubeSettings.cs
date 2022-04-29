using UnityEngine;

[CreateAssetMenu(fileName = "CubeSettings", menuName = "ScriptableObjects/CubeSettings", order = 1)]
public class CubeSettings : ScriptableObject
{
    public int Id => _id;
    public int Score => _score;
    public string ScoreText => _scoreText;
    public Material Material => _material;
    public CubeSettings NextSettings => _nextSettings;

    [SerializeField] private int _id;
    [SerializeField] private int _score;
    [SerializeField] private string _scoreText;
    [SerializeField] private Material _material;
    [SerializeField] private CubeSettings _nextSettings;
}
