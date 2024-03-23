using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float walkSpeed;
    public float runSpeed;
    private float currentSpeed;   
    private Animator animator;
    private bool climb;
    private bool jump;

   
    public bool Climb { get { return climb; } set { climb = value; } }
    public bool Jump { get { return jump; } set {  jump = value; } }
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;        
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = 0;
        climb = false;
        jump = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Talk();
        }
    }


    private void FixedUpdate()
    {
        animator.SetBool("Jump", jump);
        if (GameManager.Instance.canControlLuna)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 inputValue = new Vector2(horizontal, vertical).normalized;
            Vector2 position = transform.position;
            animator.SetFloat("LookX", horizontal);
            animator.SetFloat("LookY", vertical);
            animator.SetBool("climb", climb);
            animator.SetBool("Jump", jump);
            SetIdleDirection(inputValue, animator);
            HasAxisInput(horizontal, vertical);
            if (!climb)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    currentSpeed = runSpeed;
                    animator.SetBool("pressingShift", true);
                }
                else
                {
                    currentSpeed = walkSpeed;
                    animator.SetBool("pressingShift", false);
                }
                position = transform.position;
                position += inputValue * Time.fixedDeltaTime * currentSpeed;
                rigidbody2d.MovePosition(position);
            }
            else if (climb)
            {
                position = transform.position;
                position += inputValue * Time.fixedDeltaTime * walkSpeed;
                rigidbody2d.MovePosition(position);
            }
        }        
    }   

    public bool HasAxisInput(float x, float y)
    {
        if (x == 0 & y == 0)
        {
            animator.SetBool("hasInput", false);
            return false;
        }
        else
        {
            animator.SetBool("hasInput", true);
            return true;
        }
    }
    
    public void SetIdleDirection(Vector2 input, Animator animator)
    {
        float x = input.x;
        float y = input.y;
        if(x > 0 && y > 0)
        {
            animator.SetFloat("LookDirectionX", 1);
            animator.SetFloat("LookDirectionY", 1);
        }
        else if (x > 0 && y < 0)
        {
            animator.SetFloat("LookDirectionX", 1);
            animator.SetFloat("LookDirectionY", -1);
        }
        else if (x > 0 && y == 0)
        {
            animator.SetFloat("LookDirectionX", 1);
            animator.SetFloat("LookDirectionY", 0);
        }
        else if (x == 0 && y < 0)
        {
            animator.SetFloat("LookDirectionX", 0);
            animator.SetFloat("LookDirectionY", -1);
        }
        else if (x == 0 && y > 0)
        {
            animator.SetFloat("LookDirectionX", 0);
            animator.SetFloat("LookDirectionY", 1);
        }
        else if (x < 0 && y > 0)
        {
            animator.SetFloat("LookDirectionX", -1);
            animator.SetFloat("LookDirectionY", 1);
        }
        else if (x < 0 && y < 0)
        {
            animator.SetFloat("LookDirectionX", -1);
            animator.SetFloat("LookDirectionY", -1);
        }
        else if (x < 0 && y == 0)
        {
            animator.SetFloat("LookDirectionX", -1);
            animator.SetFloat("LookDirectionY", 0);
        }
    }

    public void Talk()
    {
        Collider2D collider = Physics2D.OverlapCircle(rigidbody2d.position, 0.4f, LayerMask.GetMask("NPC"));
        if (collider != null)
        {
            if (collider.name == "Nala")
            {
                GameManager.Instance.canControlLuna = false;
                collider.GetComponent<Dialogue>().DisplayDialogue();
            }
            else if (collider.name == "Dog" && !GameManager.Instance.hasPetTheDog && GameManager.Instance.dialogueInfoIndex == 2)
            {
                PetDog();
                GameManager.Instance.canControlLuna = false;
                collider.GetComponent<Dog>().BeHappy();
            }
        }
    }

    public void PetDog()
    {
        animator.CrossFade("PetTheDog", 1.75f);
        transform.position = new Vector3(1.891f, -3.333f, 0);
        GameManager.Instance.PlaySound(GameManager.Instance.finishTaskSound);
    }
}
