using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;//creates a varible to put the player health component in
    [SerializeField] private Image currentHealthbar;//creates a variable to place the currentHealthbar image in
    [SerializeField] private Image totalHealthbar;//creates a variable to place the totalHealthbar image in

    private void Start()
    {
        totalHealthbar.fillAmount = playerHealth.currentHealth / 10;//starts the game with the totalHealthbar fillamout according to how much health the player's current health is
    }
    private void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / 10;//updates the healthbar whenever th player takes damage and changes the according to how much damage is taken
    }
}
