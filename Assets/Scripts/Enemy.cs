using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed;
    private Vector3 position;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _attackRadius;

    private bool _isAttackTarget;
    public float AttackRadius => _attackRadius;
    public bool IsAttackTarget => _isAttackTarget;

    [SerializeField] private Controller _target;

    public Controller Target { get => _target; }

    [SerializeField] private ShootingAttack _shootingAttack;

    private void Start()
    {

        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        StartCoroutine(Coroutine());
    }
    void FixedUpdate()
    {
        Vector3 newPosition = transform.position + (-Vector3.forward) * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition.z);
    }


    public IEnumerator Coroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfAttack();
        }
    }

    private void FieldOfAttack()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _attackRadius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _targetMask))
            {
                Debug.Log("I attack target");
                _isAttackTarget = true;
                _shootingAttack.Shoot();
            }
            else
            {
                _isAttackTarget = false;
            }
        }
        else if (_isAttackTarget)
        {
            _isAttackTarget = false;
        }
    }
}
