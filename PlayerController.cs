// نمونه اولیه یک بازی ساده پلتفرمر دوبعدی در یونیتی

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // سرعت حرکت بازیکن
    public float jumpForce = 5f; // نیروی پرش
    private Rigidbody2D rb;
    private bool isGrounded;

    public int score = 0; // امتیاز بازیکن
    public UnityEngine.UI.Text scoreText; // نمایش امتیاز در UI

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateScoreText();
    }

    void Update()
    {
        // حرکت افقی بازیکن
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // چرخش بازیکن بر اساس جهت حرکت
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // پرش بازیکن
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // بررسی برخورد بازیکن با زمین
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // جمع‌آوری آیتم‌ها
        if (other.gameObject.CompareTag("Collectible"))
        {
            score += 10; // افزایش امتیاز
            Destroy(other.gameObject); // حذف آیتم
            UpdateScoreText(); // به‌روزرسانی نمایش امتیاز
        }
        // برخورد با دشمن
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over!");
            // می‌توانید اینجا سیستم پایان بازی را اضافه کنید
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
