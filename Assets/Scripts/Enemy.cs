using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4f;

    int points = 10;

    Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5)
        {
            float randomXValue = Random.Range(-9f, 9f);
            float randomYValue = Random.Range(6f, 2f);
            transform.position = new Vector3(randomXValue, randomYValue, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.ManageScore(points);
            }
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Player"))
        {
            if (_player != null)
            {
                _player.DamagePlayer();
            }
            Destroy(this.gameObject);
        }
    }
}
