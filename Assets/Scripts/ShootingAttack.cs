using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAttack : MonoBehaviour
{
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField, Min(0f)] private float _force;

    [SerializeField, Min(0f)] private float _rateOfFire;
    private float _timeToReload;

    public void Shoot()
    {
        if (_timeToReload <= 0)
        {
            var projectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
            projectile.Rigidbody.AddForce(_projectileSpawnPoint.forward * _force, _forceMode);
            _timeToReload = _rateOfFire;
        }
        else
        {
            _timeToReload -= Time.deltaTime;
        }
    }
}
