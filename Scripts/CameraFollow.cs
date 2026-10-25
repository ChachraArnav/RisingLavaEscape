using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float yOffset = 2f;
    public float smoothSpeed = 3f;

    void LateUpdate()
    {
        if (player == null) return;
        float targetY = player.position.y + yOffset;
        if (targetY > transform.position.y)
        {
            float newY = Mathf.Lerp(
                transform.position.y,
                targetY,
                smoothSpeed * Time.deltaTime
            );
            transform.position = new Vector3(
                transform.position.x,
                newY,
                transform.position.z
            );
        }
    }
}