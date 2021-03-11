using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float moveSpeed = 9f;
    public float turnSmoothTime = 0.1f;
    private Vector3 velocity;
    private float turnSmoothVelocity;
    public bool attacking = false;
    private Animator anim;
    public Health health;
    public AIHealth oneAttack;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            anim.SetTrigger("HealthCheck");
        }
        else
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }

            if (Input.GetKeyDown(KeyCode.Mouse2) && isGrounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Dodge();
            }
        }
    }

    void Move()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            moveSpeed = 9f;
            attacking = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
        {
            moveSpeed = 11f;
            attacking = false;
        }
        else
        {
            moveSpeed = 2f;
            attacking = true;
        }

        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);




        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
            anim.SetFloat("Speed", 1f, 0.07f, Time.deltaTime);
        }
        else
        {
            Idle();
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }


    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    void Idle()
    {
        anim.SetFloat("Speed", 0f, 0.07f, Time.deltaTime);
    }

    void Attack()
    {
        oneAttack.gotHitFlag = false;
        anim.SetTrigger("Attack");
    }

    void Dodge()
    {
        anim.SetTrigger("Dodge");
    }
}
