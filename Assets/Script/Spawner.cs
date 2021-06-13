using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawn;
    [SerializeField]
    private GameObject _spawnCarOne;
    [SerializeField]
    private GameObject _spawnCarTree;
    private int _sayi;



    void Start()
    {

        SpawnObje();
    }

    private void SpawnObje()
    {
        _sayi = Random.Range(0, 3);
        if (_sayi == 1)
        {
            Instantiate(_spawn, gameObject.transform.position, _spawn.transform.rotation);
        }
        else if (_sayi == 2)
        {
            Instantiate(_spawnCarOne, transform.position, _spawnCarOne.transform.rotation);
        }
        else
        {
            Instantiate(_spawnCarTree, transform.position, _spawnCarTree.transform.rotation);
        }

    }



}


