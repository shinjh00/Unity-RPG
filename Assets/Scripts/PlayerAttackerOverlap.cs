using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttackerOverlap : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float range;
    [SerializeField] int damage;


    [SerializeField] Weapon weapon;

    private void Attack()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            animator.SetTrigger("Attack1");
        }
        else
        {
            animator.SetTrigger("Attack2");
        }
    }

    Collider[] colliders = new Collider[20];
    private void AttackTiming()
    {
        // AttackTiming 때 Overlap(여러번)하고
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders, layerMask);
        for (int i = 0; i < size; i++)
        {
            // 모든 충돌체에게 데미지 주기
            /*IDamagable damagable = colliders[i].GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
            }*/

            IDamagable damagable = colliders[i].GetComponent<IDamagable>();
            damagable?.TakeDamage(damage);
        }
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
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        Attack();
    }
}
