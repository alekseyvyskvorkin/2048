using UnityEngine;

public class InputController : MonoBehaviour
{
    public Cube CurrentCube { get; set; }

    [SerializeField] private float _minPositionX = -11f;
    [SerializeField] private float _maxPositionX = 11f;
    [SerializeField] private float _moveSpeed = 200f;

    private float _positionX;

    private RaycastHit _hit;
    private Ray _ray = new Ray();

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            _ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);
            MoveCube();
            if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                Shoot();
            }
        }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            MoveCube();            
        }
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
#endif
    }

    private void MoveCube()
    {
        if (Physics.Raycast(_ray, out _hit) && CurrentCube != null)
        {
            _positionX = _hit.point.x;
            _positionX = Mathf.Clamp(_positionX, _minPositionX, _maxPositionX);

            CurrentCube.transform.localPosition = Vector3.MoveTowards(CurrentCube.transform.localPosition,
                new Vector3(_positionX, CurrentCube.transform.localPosition.y, CurrentCube.transform.localPosition.z), _moveSpeed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        if (CurrentCube != null)
        {
            CurrentCube.CubePhysics.Shoot();
            CurrentCube = null;
        }
    }
}
