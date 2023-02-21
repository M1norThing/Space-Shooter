using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f;

    [SerializeField] float _upperBound = 0f;
    [SerializeField] float _bottomBound = -3.8f;
    [SerializeField] float _leftBound = -11f;
    [SerializeField] float _rightBound = 11f;

    [SerializeField] GameObject _laserPrefab;

    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = -1f;

    Vector3 laserPositionOffset = new Vector3(0, 0.8f, 0);

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        { 
            FireLaser();
        }
    }

    private void CalculateMovement()
    {
        float horizpntalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizpntalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // limiting Y position between upper and bottom bounds (instead of an if statement)
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y, _bottomBound, _upperBound), 0);

        if (transform.position.x <= _leftBound)
        {
            transform.position = new Vector3(_rightBound, transform.position.y, 0);
        }
        else if (transform.position.x >=_rightBound)
        {
            transform.position = new Vector3(_leftBound, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + laserPositionOffset, Quaternion.identity);
    }
}
