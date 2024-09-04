using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashTime = 0.2f;
    private float dashTimer = 0f;
    public float jumpForce = 5f;
    public float saltoSpeed = 360f; // Prędkość rotacji podczas salta

    private AudioSource audioSource;
    public AudioClip dashSound;

    public Transform cameraTransform; // Referencja do Transformu kamery

    private bool isDashing = false;
    private bool isJumping = false;
    private bool isSalto = false;

    private Vector3 dashDirection;
    private Rigidbody rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDashing)
        {
            Dash();
        }
        else
        {
            Move();
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartDash();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }

        if (isJumping && isSalto)
        {
            PerformSalto();
        }
    }

    void Move()
    {
        // Pobieranie osi ruchu
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Obliczanie kierunku ruchu względem kamery
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Usuwamy komponenty osi Y, aby ruch był tylko po płaszczyźnie
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Kierunek ruchu uwzględniający pozycję kamery
        Vector3 move = forward * moveZ + right * moveX;

        if (move != Vector3.zero)
        {
            transform.position += move * moveSpeed * Time.deltaTime;
            // Ustawienie rotacji postaci zgodnie z kierunkiem ruchu
            transform.rotation = Quaternion.LookRotation(move);
        }
    }

    void StartDash()
    {
        // Odtwórz dźwięk dasha
        if (dashSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dashSound);
        }

        // Obliczenie kierunku dasha uwzględniając kamerę
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Kierunek dasha to kombinacja osi wejścia i kierunku kamery
        dashDirection = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")).normalized;
        if (dashDirection != Vector3.zero)
        {
            isDashing = true;
            dashTimer = dashTime;
        }
    }

    void Dash()
    {
        if (dashTimer > 0)
        {
            transform.position += dashDirection * dashSpeed * Time.deltaTime;
            dashTimer -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
        }
    }

    void Jump()
    {
        isJumping = true;
        isSalto = true; // Rozpoczęcie salta
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void PerformSalto()
    {
        // Obracanie kapsuły w osi X podczas skoku
        transform.Rotate(Vector3.right, saltoSpeed * Time.deltaTime);

        // Można tutaj dodać logikę, aby zatrzymać salto w momencie lądowania lub gdy rotacja przekroczy pewną wartość
    }

    void OnCollisionEnter(Collision collision)
    {
        // Gdy postać dotyka ziemi, resetuje flagi skoku i salta
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            isJumping = false;
            isSalto = false; // Zakończenie salta po lądowaniu
        }
    }
}