using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const float PunchDuration = 0.2f;

    public UiService UiService { get; set; }
    public CubeSettings CubeSettings { get; set; }
    public CubePhysics CubePhysics => _cubePhysics;
    public ParticleSystem Particle => _particle;

    [SerializeField] private CubePhysics _cubePhysics;

    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private TMP_Text[] _countText;

    private MeshRenderer _mesh;

    private Vector3 _punchScale = Vector3.one * 2f;

    public void Init(UiService service)
    {
        _cubePhysics.Init();
        UiService = service;
        _mesh = GetComponent<MeshRenderer>();
    }

    public void SwitchMaterials()
    {
        _mesh.sharedMaterial = CubeSettings.Material;
        foreach (var text in _countText)
        {
            text.text = CubeSettings.ScoreText;
        }
    }   

    private void OnDisable()
    {
        transform.rotation = Quaternion.identity;
    }

    private void Merge(Cube cube)
    {
        UiService.AddScore(CubeSettings.Score * 2);

        cube.CubePhysics.DeactivatePhysics(true);
        CubePhysics.DeactivatePhysics(true);

        cube.Particle.Play();

        if (CubeSettings.NextSettings != null)
        {
            CubeSettings = CubeSettings.NextSettings;
            if (CubeSettings.Id > CubeSpawner.MaxSettingsSpawnIndex)
            {
                CubeSpawner.MaxSettingsSpawnIndex = CubeSettings.Id;
            }
        }

        cube.CubeSettings = CubeSettings;
        cube.SwitchMaterials();
        SwitchMaterials();

        cube.transform.DOMove(transform.position, 0.1f).OnComplete(() => 
        {
            transform.DORewind();
            transform.DOPunchScale(_punchScale, PunchDuration);
            CubePhysics.DeactivatePhysics(false);
            CubePhysics.Explosion();
            cube.gameObject.SetActive(false);
        });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Cube>(out var cube)
            && cube.CubeSettings.Score == CubeSettings.Score
            && cube.CubePhysics.HasContact == false)
        {
            Merge(cube);
        }
    }
}
