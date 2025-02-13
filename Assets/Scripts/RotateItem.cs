using UnityEngine;

public class RotateItem : MonoBehaviour
{
    private Vector2 previousTouchPosition;
    private bool isRotating = false;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_OSX || UNITY_STANDALONE_WIN
        PC();
#else
        Mobile();
#endif
    }

    void PC()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousTouchPosition = Input.mousePosition;
            isRotating = true;
        }
        else if (Input.GetMouseButton(0) && isRotating)
        {
            Vector2 currentTouchPosition = Input.mousePosition;
            Vector2 touchDelta = currentTouchPosition - previousTouchPosition;
            previousTouchPosition = currentTouchPosition;

            float rotationSpeed = 0.4f; // Adjust this value to control rotation speed

            // Horizontal rotation (around y-axis)
            float horizontalAngle = touchDelta.x * rotationSpeed;
            transform.Rotate(Vector3.up, horizontalAngle, Space.World);

            // Vertical rotation (around x-axis)
            float verticalAngle = touchDelta.y * rotationSpeed;
            transform.Rotate(Vector3.right, -verticalAngle, Space.World); // Negative for intuitive rotation
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }
    }

    void Mobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;
                isRotating = true;
            }
            else if (touch.phase == TouchPhase.Moved && isRotating)
            {
                Vector2 currentTouchPosition = touch.position;
                Vector2 touchDelta = currentTouchPosition - previousTouchPosition;
                previousTouchPosition = currentTouchPosition;

                float rotationSpeed = 0.4f; // Adjust this value to control rotation speed

                // Horizontal rotation (around y-axis)
                float horizontalAngle = touchDelta.x * rotationSpeed;
                transform.Rotate(Vector3.up, horizontalAngle, Space.World);

                // Vertical rotation (around x-axis)
                float verticalAngle = touchDelta.y * rotationSpeed;
                transform.Rotate(Vector3.right, -verticalAngle, Space.World); // Negative for intuitive rotation
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isRotating = false;
            }
        }
    }
}
