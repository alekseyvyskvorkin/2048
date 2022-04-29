using UnityEngine;

public class LoseLine : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;

    private UiService _uiService;

    private void Awake()
    {
        _uiService = FindObjectOfType<UiService>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<CubePhysics>(out var cube))
        {
            if (cube.IsOutsideStartArea == true)
            {
                _uiService.UnFadeLosePanel();
            }
            else
            {
                _spawner.TryShowAds();
                _spawner.SpawnCube();
            }            
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<CubePhysics>(out var cube) 
            && cube.IsOutsideStartArea == false)
        {                        
            cube.IsOutsideStartArea = true;
        }
    }
}
