using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
        
    }

    public float GetDamage()
    {
        return damage;
    }
    
    public void Hit()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Attacker attacker = other.gameObject.GetComponent<Attacker>();
        Health health = other.gameObject.GetComponent<Health>();
        if (attacker && health)
        {
            health.DealDamage(damage);
            Hit();
        }
    }
}
