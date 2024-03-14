using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _damage;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ProjectileDisposeType _disposeType;

    private bool _isProjectileDisposed; //????, ??????????? ??? ?? ?????? ????????? ??? ???


    public float Damage => _damage;
    public Rigidbody Rigidbody => _rigidbody;
    public ProjectileDisposeType DisposeType => _disposeType;
    public bool IsProjectileDisposed => _isProjectileDisposed;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isProjectileDisposed) return; //???? ?????? ??? ????????? - ??????? ?? ??????

        if (collision.gameObject.TryGetComponent(out Controller damageable))
        {
            OnTargetCollision(collision, damageable);

            if (ProjectileDisposeType.OnTargetCollision == _disposeType)
            {
                DisposeProjectile();
            }
        }
        else
        {
            OnOtherCollision(collision);
        }

        OnAnyCollision(collision);

        if (ProjectileDisposeType.OnAnyCollision == _disposeType) DisposeProjectile();
    }

    public void DisposeProjectile()
    {
        Destroy(gameObject);
        _isProjectileDisposed = true;
    }

    protected virtual void OnTargetCollision(Collision collision, Controller damageable) { }
    protected virtual void OnOtherCollision(Collision collision) { }
    protected virtual void OnAnyCollision(Collision collision) { }
}
