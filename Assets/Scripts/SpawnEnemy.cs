using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _prefabEnemy;
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            Instantiate(_prefabEnemy, transform.position, Quaternion.identity);
        }
    }
}
