using UnityEngine;


public class Player : Fighter
{
    // Ссылки
    Rigidbody rb;
    Animator animator;

    [HideInInspector] public Vector3 moveDirection;
    Vector3 mousePosition;
    public float moveSpeed = 5f;
    public float turnSpeed;


    //---------------------------------------------------------------------------------------------------------------------------------------------------------\\


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Перемещение и направление
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;                    // скорость нормализированная
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);        // положение мыши
    }


    private void FixedUpdate()
    {
        // Скорость
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.z * moveSpeed);                 // скорость полная

        // Направление
        Vector3 aimDirection = mousePosition - rb.position;                                                     // находим угол в градусах
        float aimAngle = Mathf.Atan2(aimDirection.x, aimDirection.z) * Mathf.Rad2Deg;                                 
        Quaternion qua1 = Quaternion.Euler(transform.eulerAngles.x, aimAngle, transform.eulerAngles.z);         // создаем этот угол           
        rb.rotation = Quaternion.Lerp(transform.rotation, qua1, Time.fixedDeltaTime * turnSpeed);                      // делаем Lerp           

        // Анимации
        if (rb.velocity.magnitude > 0.5f)
        {
            animator.SetBool("Runing", true);
        }
        else
        {
            animator.SetBool("Runing", false);
        }
    }
}