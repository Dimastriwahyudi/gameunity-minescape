using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (Time.timeScale == 0f) return;

        Vector3 targetPos = player.position;
        targetPos.z = transform.position.z;

        float clampedX = Mathf.Clamp(targetPos.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(targetPos.y, minBounds.y, maxBounds.y);
        Vector3 clampedPos = new Vector3(clampedX, clampedY, targetPos.z);

        transform.position = Vector3.SmoothDamp(transform.position, clampedPos, ref velocity, smoothTime);
    }
}
