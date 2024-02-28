using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Weapon weapon;

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void EnableWeapon()
    {
        weapon.EnableWeapon();
    }

    public void DisableWeapon()
    {
        weapon.DisableWeapon();
    }

    private void OnAttack(InputValue value)
    {
        Attack();
    }
}
