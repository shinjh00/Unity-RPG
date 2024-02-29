using UnityEngine;
using UnityEngine.Events;

public class DamagableAdapter : MonoBehaviour, IDamagable
{
    public UnityEvent<int> OnTakeDamage;

    public void TakeDamage(int damage)
    {
        OnTakeDamage?.Invoke(damage);
    }
}
