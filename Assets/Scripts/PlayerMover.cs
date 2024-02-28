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
        /* 1 - 카메라가 비추는 방향 기준으로 앞뒤좌우 움직이기 */

        Vector3 forwardDir = Camera.main.transform.forward;
        forwardDir = new Vector3(forwardDir.x, 0, forwardDir.z).normalized;

        Vector3 rightDir = Camera.main.transform.right;
        rightDir = new Vector3(rightDir.x, 0, rightDir.z).normalized;
        // y는 0으로 세팅하여 땅과 평행하게 만들기
        // normalized : 벡터의 크기를 1로 정규화

        controller.Move(forwardDir * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(rightDir * moveDir.x * moveSpeed * Time.deltaTime);


        /* 2 - 입력하는 방향쪽을 바라보게 회전해주기 */

        Vector3 lookDir = forwardDir * moveDir.z + rightDir * moveDir.x;
        // z축 쪽으로 갈 땐 -> forwardDir * moveDir.z  // x축 쪽으로 갈 땐 -> rightDir * moveDir.x
        // 두 입력을 합친 쪽의 방향으로 회전하기
        if (lookDir.sqrMagnitude > 0)  // if (lookDir != Vector3.zero) 이게 더 빠름
        {
            // 입력이 있을 경우
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10);  // 선형 보간
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
