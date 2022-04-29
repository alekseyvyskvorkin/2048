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
        if (Input.touchCount > 0)
        {
            MoveCube();
            OnCompleteTouch();
        }
    }

    private void MoveCube()
    {
        _ray = _camera.ScreenPointToRay(Input.GetTouch(0).position);

        if (Physics.Raycast(_ray, out _hit) && CurrentCube != null)
        {
            _positionX = _hit.point.x;
            _positionX = Mathf.Clamp(_positionX, _minPositionX, _maxPositionX);

            CurrentCube.transform.localPosition = Vector3.MoveTowards(CurrentCube.transform.localPosition,
                new Vector3(_positionX, CurrentCube.transform.localPosition.y, CurrentCube.transform.localPosition.z), _moveSpeed * Time.deltaTime);
        }
    }

    private void OnCompleteTouch()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
        {
            if (CurrentCube != null)
            {
                CurrentCube.CubePhysics.Shoot();
                CurrentCube = null;
            }
        }
    }
}
