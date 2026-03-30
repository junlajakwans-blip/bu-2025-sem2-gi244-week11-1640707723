using UnityEngine;
using UnityEngine.InputSystem;

// 1.1 START HERE
public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;

    private InputAction moveAction;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }
    void Update()
    {
        float input = moveAction.ReadValue<Vector2>().x;

        transform.Rotate(Vector3.up, input * rotationSpeed * Time.deltaTime);
    }
}
