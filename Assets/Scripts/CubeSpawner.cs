using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    private const int StartSpawnCount = 17;
    private const int MaxSpawnsToShowAds = 21;
    private const int MinSpawnsToShowAds = 10;

    public static int MaxSettingsSpawnIndex;

    [SerializeField] private AdsInterstitial _adsInterstitial;

    [SerializeField] private Cube[] _cubes;
    [SerializeField] private InputController _inputController;
    [SerializeField] private CubeSettings[] _cubeSettings;

    [SerializeField] private int _maximumStartIndex = 2;
    [SerializeField] private float _spawnDelay = 0.1f;

    [SerializeField] private List<Vector3> _restartSpawnPoints = new List<Vector3>();

    private int _spawnCount;
    private int _countToShowAds;

    private void Awake()
    {
        var service = FindObjectOfType<UiService>();
        foreach (var cube in _cubes)
        {
            cube.Init(service);
        }
    }

    public void Play() => StartCoroutine(StartGame());

    public void Restart() => StartCoroutine(RestartGame());

    public void SpawnCube()
    {
        for (int i = 0; i < _cubes.Length; i++)
        {
            if (_cubes[i].gameObject.activeInHierarchy == false)
            {
                _cubes[i].transform.position = _restartSpawnPoints[0];
                _cubes[i].transform.localScale = Vector3.one;
                _cubes[i].CubeSettings = _cubeSettings[Random.Range(0, MaxSettingsSpawnIndex)];
                _cubes[i].SwitchMaterials();
                _cubes[i].gameObject.SetActive(true);
                _inputController.CurrentCube = _cubes[i];
                break;
            }
        }
    }

    public void TryShowAds()
    {
        _spawnCount++;
        if (_spawnCount >= _countToShowAds)
        {
            _adsInterstitial.ShowInterstitial();
            _countToShowAds = Random.Range(MinSpawnsToShowAds, MaxSpawnsToShowAds);
            _spawnCount = 0;
        }
    }

    private IEnumerator RestartGame()
    {
        foreach (var cube in _cubes)
        {
            cube.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f);

        Play();
    }

    private IEnumerator StartGame()
    {
        for (int i = 0; i < StartSpawnCount; i++)
        {
            _cubes[i].gameObject.SetActive(true);
            _cubes[i].transform.position = _restartSpawnPoints[i];
            _cubes[i].CubeSettings = _cubeSettings[Random.Range(0, _maximumStartIndex)];
            _cubes[i].SwitchMaterials();
            yield return new WaitForSeconds(_spawnDelay);
        }

        MaxSettingsSpawnIndex = _maximumStartIndex;
        _countToShowAds = Random.Range(MinSpawnsToShowAds, MaxSpawnsToShowAds);

        _inputController.CurrentCube = _cubes[0];
    }
}
