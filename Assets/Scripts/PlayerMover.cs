using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed;

    private Vector3 moveDir;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        /* 1 - ī�޶� ���ߴ� ���� �������� �յ��¿� �����̱� */

        Vector3 forwardDir = Camera.main.transform.forward;
        forwardDir = new Vector3(forwardDir.x, 0, forwardDir.z).normalized;

        Vector3 rightDir = Camera.main.transform.right;
        rightDir = new Vector3(rightDir.x, 0, rightDir.z).normalized;
        // y�� 0���� �����Ͽ� ���� �����ϰ� �����
        // normalized : ������ ũ�⸦ 1�� ����ȭ

        controller.Move(forwardDir * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(rightDir * moveDir.x * moveSpeed * Time.deltaTime);


        /* 2 - �Է��ϴ� �������� �ٶ󺸰� ȸ�����ֱ� */

        Vector3 lookDir = forwardDir * moveDir.z + rightDir * moveDir.x;
        // z�� ������ �� �� -> forwardDir * moveDir.z  // x�� ������ �� �� -> rightDir * moveDir.x
        // �� �Է��� ��ģ ���� �������� ȸ���ϱ�
        if (lookDir.sqrMagnitude > 0)  // if (lookDir != Vector3.zero) �̰� �� ����
        {
            // �Է��� ���� ���
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10);  // ���� ����
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDir.x = input.x;
        moveDir.z = input.y;

        animator.SetFloat("MoveSpeed", moveDir.magnitude);
    }

}
