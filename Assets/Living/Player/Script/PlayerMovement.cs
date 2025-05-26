using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [Header("XR Settings")]
    public XRNode moveSource = XRNode.LeftHand;   // joystick pour bouger
    public XRNode actionSource = XRNode.RightHand;  // boutons A/B

    [Header("Speeds")]
    public float walkSpeed = 3f;
    public float sprintMultiplier = 2f;

    [Header("Jump & Crouch")]
    public float jumpHeight = 1.5f;
    public float crouchHeight = 0.9f;              
    private float originalHeight;
    private Vector3 originalCenter;

    [Header("Gravity")]
    public float gravity = -9.81f;

    [Header("Footsteps")]
    public AudioSource audioSource;
    public float footstepInterval = 0.5f;

    // --- états internes
    private CharacterController _cc;
    private Vector2 _moveAxis;
    private bool _isSprinting;
    private bool _isCrouching;
    private bool _jumpPressed;
    private float _verticalVelocity;
    private float _footstepTimer;
    static public bool dialogue = false ;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        // mémorise la configuration capsule d’origine
        originalHeight = _cc.height;
        originalCenter = _cc.center;
    }

    void Update()
    {
        // 1) mouvement & sprint
        var mvDev = InputDevices.GetDeviceAtXRNode(moveSource);
        mvDev.TryGetFeatureValue(CommonUsages.primary2DAxis, out _moveAxis);
        mvDev.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out _isSprinting);

        // 2) actions (A pour crouch, B pour jump)
        var actDev = InputDevices.GetDeviceAtXRNode(actionSource);
        actDev.TryGetFeatureValue(CommonUsages.primaryButton, out _isCrouching);
        actDev.TryGetFeatureValue(CommonUsages.secondaryButton, out _jumpPressed);

        // 3) ajuste la capsule pour le crouch
        if (_isCrouching)
        {
            _cc.height = crouchHeight;
            _cc.center = new Vector3(0, crouchHeight / 2f, 0);
        }
        else
        {
            _cc.height = originalHeight;
            _cc.center = originalCenter;
        }
    }

    void FixedUpdate()
    {
        bool isGrounded = _cc.isGrounded;
        if (isGrounded && _verticalVelocity < 0f)
            _verticalVelocity = -2f;  // petite force pour rester collé

        // Jump
        if (_jumpPressed && isGrounded)
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Mouvement horizontal
        float speed = walkSpeed * (_isSprinting ? sprintMultiplier : 1f);

        Vector3 fwd = Camera.main.transform.forward;
        fwd.y = 0; fwd.Normalize();
        Vector3 right = Camera.main.transform.right;
        right.y = 0; right.Normalize();

        Vector3 move = (fwd * _moveAxis.y + right * _moveAxis.x) * speed;

        // Gravité
        _verticalVelocity += gravity * Time.fixedDeltaTime;
        Vector3 gravMove = Vector3.up * _verticalVelocity;

        // Pas réalistes
        if (isGrounded && move.magnitude > 0.1f)
        {
            _footstepTimer += Time.fixedDeltaTime;
            if (_footstepTimer >= footstepInterval)
            {
                audioSource.Play();
                _footstepTimer = 0f;
            }
        }
        else
        {
            // reset timer quand on s’arrête / en l’air
            _footstepTimer = footstepInterval;
        }

        // Applique le déplacement
        _cc.Move((move + gravMove) * Time.fixedDeltaTime);
    }
}