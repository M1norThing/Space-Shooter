using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 8f;
    float _playerSpeedMultiplayer = 2f;
    [SerializeField] float _upperBound = 0f;
    [SerializeField] float _bottomBound = -3.8f;
    [SerializeField] float _leftBound = -11f;
    [SerializeField] float _rightBound = 11f;
    [SerializeField] float _fireRate = 0.5f;
    [SerializeField] int _playerHealth = 3;

    [SerializeField] GameObject _laserPrefab;
    [SerializeField] GameObject _tripleShotPrefab;
    [SerializeField] GameObject _shieldPowerUpPrefab;

    SpawnManager spawnManager;

    float _canFireAfter = -1f;
    Vector3 laserPositionOffset = new Vector3(0, 1.05f, 0);

    bool _isTripleShotActive = false;
    bool _isSpeedBoostActive = false;
    bool _isShieldActive = false;
    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFireAfter)
        { 
            FireLaser();
        }
        
    }

    private void CalculateMovement()
    {
        float horizpntalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizpntalInput, verticalInput, 0);
        transform.Translate(direction * _playerSpeed * Time.deltaTime);

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
        _canFireAfter = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + laserPositionOffset, Quaternion.identity);
        }
    }

    public void DamagePlayer()
    {

        if (_isShieldActive)
        {
            return;
        }


        _playerHealth--;

        if (_playerHealth < 1)
        {
            spawnManager.OnTriggerDeth();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotIsActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerUpRoutine());
    }

    IEnumerator TripleShotPowerUpRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostIsActive()
    {
        _isSpeedBoostActive = true;
        _playerSpeed *= _playerSpeedMultiplayer;
        StartCoroutine(SpeedBoostPowerUpRoutine());
    }

    IEnumerator SpeedBoostPowerUpRoutine()
    {
        yield return new WaitForSeconds(7f);
        _isSpeedBoostActive = false;
        _playerSpeed /= _playerSpeedMultiplayer;
    }

    public void ShieldPowerIsActive()
    {
        _isShieldActive = true;
        StartCoroutine(ShieldPowerUpRoutine());

    }

    IEnumerator ShieldPowerUpRoutine()
    {
        _shieldPowerUpPrefab.SetActive(true);
        yield return new WaitForSeconds(7f);
        _shieldPowerUpPrefab.SetActive(false);
        _isShieldActive = false;
    }


}
