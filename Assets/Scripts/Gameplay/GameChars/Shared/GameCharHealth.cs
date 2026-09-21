using UnityEngine;

public class GameCharHealth : MonoBehaviour
{
    public float MaxHealth;
    public float Health;

    private void Start()
    {
        Health = MaxHealth;    
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
