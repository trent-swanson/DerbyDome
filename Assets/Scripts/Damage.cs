//================================================================================
//Damage
//
//Purpose: To control, assign and calculate how much damage is done on impact
//
//Creator: Trent Swanson
//================================================================================

using UnityEngine;

public class Damage : MonoBehaviour
{
    Rigidbody rigidBody;

    public float attackValue = 100.0f;
    public float damageToTake;
    public float carSpeed;
    public float minAttackSpeed = 20;

    [Space]

    private float hitTimer = 3.0f;
    private float timer;

    private Score scoreScript;
    private CarController carController;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
        scoreScript = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Score>();
        carController = transform.parent.GetComponent<CarController>();
        timer = 0;
    }

    void Update()
    {
        carSpeed = carController.speed;
        damageToTake = attackValue + carSpeed;

        if(timer > 0)
            timer -= Time.deltaTime;
    }

    private void BodyScore()
    {
        scoreScript.ScoreIncrease(carController.playerID, (int)damageToTake);
    }

    private void KillScore()
    {
        scoreScript.ScoreIncrease(carController.playerID, 300);
    }

    void OnTriggerEnter(Collider other)
    {
        if (carSpeed >= minAttackSpeed && timer <= 0)
        {
            //Calculation occurs when a player hits anywhere on the other car except for the bumper
            if (other.gameObject.tag == "Player" && other.GetComponent<CarController>().isAlive)
            {
                carController.cameraShake.Shake(0.1f, 0.2f);
                StartCoroutine(carController.HueFlash(0.05f));
                other.gameObject.GetComponent<CarController>().TakeDamage(damageToTake);

                if (other.gameObject.GetComponent<CarController>().isAlive == false)
                {
                    scoreScript.killIncrease(carController.playerID);
                    KillScore();
                }

                BodyScore();
                timer = hitTimer;
                return;
            }
        }
    }
}