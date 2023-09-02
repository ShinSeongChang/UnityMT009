using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Test : MonoBehaviourPun
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    private Animator animator;
    private Rigidbody rigidbody;

    private float walkSpeed = 1.0f;
    private float runSpeed = 5.0f;
    private float jumpForce = 5.0f;

    private float offset = 0;
    public bool isGrounded;
    public bool isJump;

    private void Awake()
    {
        animator = characterBody.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ���� �÷��̾ �ƴ϶�� �Է��� ���� ����
        if (!photonView.IsMine)
        {
            return;
        }

        isGrounded = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Vector3.down, 0.5f);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Vector3.down, Color.green);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        animator.SetBool("Jump", !isGrounded);

        LookAround();
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal == 0 && vertical == 0)
        {
            offset = 0f;
        }
        else
        {
            offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;
        }

        animator.SetFloat("moveSpeed", offset);

        Vector2 moveInput = new Vector2(horizontal, vertical);

        bool isWalk = moveInput.magnitude != 0;
        if (isWalk)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            if (moveInput.magnitude != 0f)
            {
                characterBody.forward = moveDir;
            }

            // shiftŰ�� �ȴ����� �� walkSpeed, ShiftŰ�� ������ �� runSpeed ���� moveSpeed�� ����
            float moveSpeed = Mathf.Lerp(walkSpeed, runSpeed, Input.GetAxis("Sprint"));

            transform.position += moveSpeed * Time.deltaTime * Vector3.ClampMagnitude(moveDir, 1f);
        }
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        // ������ ���� ������ ���콺�� ȸ���ϴٰ� ��������.
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void Jump()
    {
        animator.SetBool("Jump", true);
        animator.SetTrigger("SpaceKey");
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}