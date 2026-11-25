using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 5.5f, -7.5f);
    [SerializeField] private float smooth = 7f;
    [SerializeField] private float lookHeight = 1.2f;

    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desired, smooth * Time.deltaTime);
        Vector3 lookPoint = target.position + Vector3.up * lookHeight;
        transform.rotation = Quaternion.Slerp(
            transform.rotation, Quaternion.LookRotation(lookPoint - transform.position),
            smooth * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget) => target = newTarget;
}
