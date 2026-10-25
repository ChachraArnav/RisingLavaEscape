using UnityEngine;

public class Coin : MonoBehaviour

{
    void Update()
    {
        transform.Rotate(0, 0, 90 * Time.deltaTime);
    }
    public GameObject collectEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddCoin();
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayCoin();
            if (collectEffect)
                Instantiate(collectEffect, transform.position,
                            Quaternion.identity);
            Destroy(gameObject);
        }
    }
}