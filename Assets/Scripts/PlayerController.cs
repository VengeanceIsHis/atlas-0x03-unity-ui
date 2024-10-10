using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private bool isInTeleporter = false;
    public Transform teleportTarget;
    public Transform teleportTarget1;
    private int score = 0;
    public float speed = 5f;
    private Rigidbody rb;
    public int health = 5;
    public float teleportCD = 2f;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        // WASD buttons enabled
        float direction_x = Input.GetAxis("Horizontal");
        float direction_z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(direction_x, 0.0f, direction_z).normalized;

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
       
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit: " + other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score += 1;
            // Debug.Log("Score: " + score);
            SetScoreText();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            Debug.Log("Health: " + health);
        }

        if (other.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }

        if (other.CompareTag("Teleporter") && !isInTeleporter)
        {
            isInTeleporter = true;
            if (other.gameObject.name == "Teleporter")
            {
                rb.position = teleportTarget1.transform.position;
            }
            else
            {
                rb.position = teleportTarget.transform.position;
            }
            StartCoroutine(TeleporterCooldown());
        }
    }

    private IEnumerator TeleporterCooldown()
    {
        Debug.Log("I am here!");
        yield return new WaitForSeconds(teleportCD);
        isInTeleporter = false;
    }


    void Update()
    {
        if (health == 0)
        {
            Debug.Log("Game Over!");

            Scene currentScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(currentScene.name);
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}