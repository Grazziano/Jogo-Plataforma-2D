using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  public float speed;

  public Transform groundCheck;
  public LayerMask layerGround;
  public float radiusCheck;
  public bool grounded;

  private bool facingRight;
  private Rigidbody2D rb2D;
  private Animator anim;
  private bool isVisible = false;

    // Start is called before the first frame update
    void Start()
    {
      rb2D = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);

       if (!grounded)
          Flip();
    }

    void FixedUpdate(){
      if (isVisible) {
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
      } else{
        rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
      }
    }

    void Flip(){
      facingRight = !facingRight;
      transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
      speed *= -1;
    }

    void OnBecameVisible(){
      Invoke("MoveEnemy", 3f);
    }
    void OnBecameInvisible(){
      Invoke("StopEnemy", 3f);
    }

    void MoveEnemy(){
      isVisible = true;
      anim.Play("Run");
    }

    void StopEnemy(){
      isVisible = false;
      anim.Play("Idle");
    }
}
