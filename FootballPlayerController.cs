using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FootballPlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private MobileJoystick joystick;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 5.2f;
    [SerializeField] private float sprintMultiplier = 1.45f;
    [SerializeField] private float turnSpeed = 12f;
    [SerializeField] private float gravity = -20f;

    [Header("Ball")]
    [SerializeField] private FootballBall ball;
    [SerializeField] private Transform kickOrigin;
    [SerializeField] private float passForce = 7f;
    [SerializeField] private float shotForce = 13f;
    [SerializeField] private float kickRange = 2.2f;

    private CharacterController controller;
    private Vector3 verticalVelocity;
    private bool sprinting;

    private void Awake() => controller = GetComponent<CharacterController>();

    private void Update()
    {
        Vector2 input = joystick != null ? joystick.Direction : Vector2.zero;
        Vector3 forward = cameraTransform != null ? cameraTransform.forward : Vector3.forward;
        Vector3 right = cameraTransform != null ? cameraTransform.right : Vector3.right;
        forward.y = 0; right.y = 0;
        forward.Normalize(); right.Normalize();

        Vector3 move = forward * input.y + right * input.x;
        if (move.sqrMagnitude > 1f) move.Normalize();

        float speed = moveSpeed * (sprinting ? sprintMultiplier : 1f);
        controller.Move(move * speed * Time.deltaTime);

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, turnSpeed * Time.deltaTime);
        }

        if (controller.isGrounded && verticalVelocity.y < 0) verticalVelocity.y = -2f;
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void SetSprint(bool value) => sprinting = value;

    public void Pass() => Kick(passForce, 0.12f);
    public void Shoot() => Kick(shotForce, 0.22f);

    private void Kick(float force, float lift)
    {
        if (ball == null) return;
        Vector3 origin = kickOrigin != null ? kickOrigin.position : transform.position + Vector3.up * 0.5f;
        if (Vector3.Distance(origin, ball.transform.position) > kickRange) return;

        Vector3 direction = transform.forward + Vector3.up * lift;
        ball.Kick(direction.normalized, force);
    }
}
