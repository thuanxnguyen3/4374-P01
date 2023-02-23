using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    [SerializeField]
    public float initialPlayerSpeed = 4f;
    [SerializeField]
    public float maximumPlayerSpeed = 30f;
    [SerializeField]
    public float playerSpeedIncreaseRate = .1f;
    [SerializeField]
    public float jumpHeight = 1.0f;
    [SerializeField]
    public float initialGravityValue = -9.81f;
    [SerializeField]
    public LayerMask groundLayer;
    [SerializeField]
    public LayerMask turnLayer;
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public AnimationClip slideAnimationClip;

    public float playerSpeed;
    public float gravity;
    public Vector3 movementDirection = Vector3.forward;
    public Vector3 playerVelocity;

    public CharacterController characterController;
    private InputManager _inputManager;
    private GameController _controller;

    [SerializeField]
    public UnityEvent<Vector3> turnEvent;

    private bool sliding = false;
    private int slidingAnimationId;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        slidingAnimationId = Animator.StringToHash("Sliding");
        _inputManager = GetComponent<InputManager>();
        _controller = GetComponent<GameController>();
    }
    
    private void Update()
    {
        characterController.Move(transform.forward * playerSpeed * Time.deltaTime);

        if(IsGrounded() && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }


        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    
    private void OnEnable()
    {

        _inputManager.OnSwipeLeft += PlayerTurnLeft;
        _inputManager.OnSwipeRight += PlayerTurnRight;
        _inputManager.OnSwipeUp += PlayerJump;
        _inputManager.OnSwipeDown += PlayerSlide;


    }

    private void OnDisable()
    {

        _inputManager.OnSwipeLeft -= PlayerTurnLeft;
        _inputManager.OnSwipeRight -= PlayerTurnRight;
        _inputManager.OnSwipeUp -= PlayerJump;
        _inputManager.OnSwipeDown -= PlayerSlide;

    }

    private void PlayerTurnLeft()
    {
        Vector3? turnPosition = CheckTurn(-1);
        Debug.Log("Left swipe detected");
        Vector3 targetDirection = Quaternion.AngleAxis(-90, Vector3.up) * movementDirection;
        turnEvent.Invoke(targetDirection);
        Turn(-1, turnPosition.Value);
    }

    private void PlayerTurnRight()
    {
        Vector3? turnPosition = CheckTurn(1);
        Debug.Log("Left swipe detected");
        Vector3 targetDirection = Quaternion.AngleAxis(90, Vector3.up) * movementDirection;
        turnEvent.Invoke(targetDirection);
        Turn(1, turnPosition.Value);

    }

    private void Turn(float turnValue, Vector3 turnPosition)
    {
        Vector3 tempPlayerPosition = new Vector3(turnPosition.x, transform.position.y, turnPosition.z);
        characterController.enabled = false;
        transform.position = tempPlayerPosition;
        characterController.enabled = true;

        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 90 * turnValue, 0);
        transform.rotation = targetRotation;
        movementDirection = transform.forward.normalized;
    }

    
    private Vector3? CheckTurn(float turnValue)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, .1f, turnLayer);
        if (hitColliders.Length != 0)
        {
            Tile tile = hitColliders[0].transform.parent.GetComponent<Tile>();
            TileType type = tile.type;
            if ((type == TileType.LEFT && turnValue == -1) ||
                (type == TileType.RIGHT && turnValue == 1) ||
                (type == TileType.SIDEWAYS))
            {
                return tile.pivot.position;
            }
        }
        return null;
    }
    

    private void PlayerJump()
    {
        Debug.Log("Up swipe detected");
        if (IsGrounded())
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * gravity * -3f);
            characterController.Move(playerVelocity * Time.deltaTime);
        }
    }

    private void PlayerSlide()
    {
        Debug.Log("Slide swipe detected");
        
        if (!sliding && IsGrounded())
        {
            StartCoroutine(Slide());
        }

    }
    
    private IEnumerator Slide()
    {
        sliding = true;
        // Shrink the collider
        Vector3 originalControllerCenter = characterController.center;
        Vector3 newControllerCenter = originalControllerCenter;
        characterController.height /= 2;
        newControllerCenter.y -= characterController.height / 2;
        characterController.center = newControllerCenter;


        // PLay the sliding animation
        animator.Play(slidingAnimationId);
        yield return new WaitForSeconds(slideAnimationClip.length);
        // Set the character controller collider back to normal after sliding
        characterController.height *= 2;
        characterController.center = originalControllerCenter;
        sliding = false;
    }

    
    public bool IsGrounded (float length = .2f)
    {
        Vector3 raycastOriginFirst = transform.position;
        raycastOriginFirst.y -= characterController.height / 2f;
        raycastOriginFirst.y += .1f;

        Vector3 raycastOriginSecond = raycastOriginFirst;
        raycastOriginFirst -= transform.forward * .2f;
        raycastOriginSecond += transform.forward * .2f;

        if (Physics.Raycast(raycastOriginFirst, Vector3.down, out RaycastHit hit, length, groundLayer) ||
            Physics.Raycast(raycastOriginSecond, Vector3.down, out RaycastHit hit2, length, groundLayer)) 
        {
            return true;
        }
        return false;
    }
}
