using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private Transform _playerCar;
    private void Update()
    {
        CarController();
    }

    private void CarController()
    {
        if (_playerCar.transform.position.z > transform.position.z + 60)
        {
            transform.position += new Vector3(0, 0, _playerCar.transform.position.z - transform.position.z + 500);
        }
    }
}
