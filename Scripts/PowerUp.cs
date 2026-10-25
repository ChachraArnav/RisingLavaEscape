using UnityEngine;

public enum PowerUpType { Shield, JumpBoost, TimeSlow }

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float duration = 5f;

    void Update()
    {
        transform.Rotate(0, 0, 45 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayPowerUp();
        if (!other.CompareTag("Player")) return;

        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        LavaRise lava = FindObjectOfType<LavaRise>();

        switch (type)
        {
            case PowerUpType.Shield:
                GameManager.Instance.ActivateShield(duration);
                GameManager.Instance.ShowPowerUpText(
                    "Shield Active", duration);
                break;

            case PowerUpType.JumpBoost:
                if (pm != null)
                    StartCoroutine(BoostJump(pm));
                GameManager.Instance.ShowPowerUpText(
                    "Jump Boost", duration);
                break;

            case PowerUpType.TimeSlow:
                if (lava != null)
                    lava.ApplySlow(duration);
                GameManager.Instance.ShowPowerUpText(
                    "Lava Slowed", duration);
                break;
        }

        Destroy(gameObject);
    }

    System.Collections.IEnumerator BoostJump(PlayerMovement pm)
    {
        pm.jumpBoosted = true;
        yield return new WaitForSeconds(duration);
        pm.jumpBoosted = false;
    }
}