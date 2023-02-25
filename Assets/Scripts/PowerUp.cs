using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float powerUpFallSpeed = 3f;

    void Update()
    {
        transform.Translate(Vector3.down * powerUpFallSpeed * Time.deltaTime);
        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TripleShotIsActive();
            }
            else
            {
                Debug.LogError("Player is NULL");
            }
            Destroy(this.gameObject);
        }
    }
}
