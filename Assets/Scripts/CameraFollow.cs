using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 originalOffset = new Vector3(0f, 1f, -10f);
    private Vector3 jumpOffset = new Vector3(0f, 1f, -10f);
    private Vector3 offset;
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void OnEnable()
    {
        Player.OnJump += HandlePlayerJump;
    }

    private void OnDisable()
    {
        Player.OnJump -= HandlePlayerJump;
    }

    private void Start()
    {
        offset = originalOffset;
    }

    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void HandlePlayerJump()
    {
        StopAllCoroutines();
        StartCoroutine(AdjustCameraOnJump());
    }

    private IEnumerator AdjustCameraOnJump()
    {
        offset = jumpOffset;
        yield return new WaitForSeconds(0.5f); // Keep the camera back for 0.5 seconds
        offset = originalOffset;
    }
}
