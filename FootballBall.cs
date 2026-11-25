using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FootballBall : MonoBehaviour
{
    [SerializeField] private float groundDrag = 0.7f;
    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.mass = 0.43f;
        body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void FixedUpdate()
    {
        Vector3 horizontal = new Vector3(body.velocity.x, 0, body.velocity.z);
        if (horizontal.sqrMagnitude > 0.01f)
            body.AddForce(-horizontal * groundDrag, ForceMode.Acceleration);
    }

    public void Kick(Vector3 direction, float force)
    {
        body.AddForce(direction * force, ForceMode.Impulse);
    }

    public void ResetBall(Vector3 position)
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.position = position;
    }
}
