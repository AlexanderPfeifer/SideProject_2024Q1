using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int currentHealthPoints;
    [SerializeField] private int maxHealthPoints;

    public bool isDead;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }

    //When executed needs an int as the damage input, then subtracts the damage from the currentHealth and also asks if player has no health left
    public void GetDamage(int damage)
    {
        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            isDead = true;
        }
    }
}
