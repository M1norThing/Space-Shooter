using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float powerUpFallSpeed = 3f;
    [SerializeField] int powerUpID;
    [SerializeField] AudioClip _audioCLip;

   

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

            AudioSource.PlayClipAtPoint(_audioCLip, transform.position);

            if (player != null)
            {
                switch (powerUpID)
                {
                    case 0: player.TripleShotIsActive(); break;
                    case 1: player.SpeedBoostIsActive(); break;
                    case 2: player.ShieldPowerIsActive(); break;
                    default: Debug.Log("Defaul value"); break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
