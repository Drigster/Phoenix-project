using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    public int Health => _health;
    public bool IsDead => _health <= 0;

    public void Damage(int damage)
    {
        if(_health - damage < 0)
        {
            _health = 0;
        }
        else
        {
            _health -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Damage(1);
            Destroy(collision.gameObject);
        }
    }
}
