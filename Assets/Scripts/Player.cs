using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController gc;
    private CharacterController controller;
    private bool isMoveLeft;
    private bool isMoveRight;
    public bool isDead;
    private float jumpVelocity;
    public float speed;
    public float jumpHeight;
    public float gravity;
    public float horizontalSpeed;
    public float rayRadius;
    public LayerMask layer;
    public LayerMask layerCoin;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gc = FindObjectOfType<GameController>();
    }


    void Update()
    {

        Vector3 direction = Vector3.forward * speed; //forward adiciona 1 no eixo Z
        if (controller.isGrounded)
        {
            //keydown é 1 e 0 como se fosse o just pressed do gdscript
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVelocity = jumpHeight;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 3f && !isMoveRight)
            {
                isMoveRight = true;
                StartCoroutine(RightMove());
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -3f && !isMoveLeft)
            {
                isMoveLeft = true;
                StartCoroutine(LeftMove());
            }
        }
        else
        {
            jumpVelocity -= gravity;
        }
        //movimentacao do player
        direction.y = jumpVelocity;
        controller.Move(direction * Time.deltaTime);

        OnCollision();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    IEnumerator LeftMove()
    {
        for (float i = 0; i < 5; i += 0.1f)
        {
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMoveLeft = false;
    }
    IEnumerator RightMove()
    {
        for (float i = 0; i < 5; i += 0.1f)
        {
            controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMoveRight = false;
    }

    void OnCollision()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadius, layer) && !isDead)
        {
            //Chama o gameover

            anim.SetTrigger("die");
            speed = 0;
            jumpHeight = 0;
            horizontalSpeed = 0;
            Invoke("GameOver", 3f); //invoke da um atraso ao chamar um comando

            isDead = true;
        }

        RaycastHit hitCoin;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitCoin, rayRadius, layerCoin))
        {
            //ao colidir com a moeda
            gc.AddCoin();
            Destroy(hitCoin.transform.gameObject);


        }
    }
    void GameOver()
    {
        gc.ShowGameOver();
    }
}
