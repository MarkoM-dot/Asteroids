using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ParticleSystem explosion;
    public Player player;
    public int score = 0;
    [SerializeField] int lives = 3;
    [SerializeField] float respawnTime = 3.0f;


    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        
    }
    
    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        
        lives--;

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {

            Invoke(nameof(Respawn), respawnTime);
        }
    }

    private void Respawn()
    {
        player.transform.position = Vector2.zero;
        player.gameObject.SetActive(true);
        player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        Invoke(nameof(TurnOnCollisions), 3.0f);
    }

    private void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {

    }
}
