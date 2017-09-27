using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPersonaje : MonoBehaviour
{
    public float maxVel = 5f;
    public float jump = 1f;
    public Slider slider;
    public Text txt;
    public float energy = 100f;
    public int premioEnemigo = 15;
    public int numGolpes = 3;

    Rigidbody2D rgb;
    Animator anim;
    bool haciaDerecha = true;
    ControlEnemigo ctrEnem;
    int golpes;

    // Use this for initialization
    void Start ()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Fire1")) > 0.01f)
        {
            anim.SetTrigger("atacar");
            golpes++;
            if (ctrEnem != null)
            {
                if (ctrEnem.GolpeIndio() == true)
                {
                    energy = energy + premioEnemigo;
                }
            }
        }

        slider.value = energy;
        txt.text = energy.ToString();
    }

    void FixedUpdate ()
    {
        float v = Input.GetAxis("Horizontal");
        Vector2 vel = new Vector2(0, rgb.velocity.y);
        v *= maxVel;
        vel.x = v;
        rgb.velocity = vel;

        if (haciaDerecha == true && v < 0)
        {
            haciaDerecha = false;
            Flip();
        }
        else if (haciaDerecha == false && v > 0)
        {
            haciaDerecha = true;
            Flip();
        }

        if (Input.GetAxis("Jump") > 0)
        {
            rgb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }

        //if (Mathf.Abs(Input.GetAxis("Fire1")) > 0.01f)
        //{
        //    anim.SetTrigger("atacar");
        //    golpes++;
        //    if (ctrEnem != null)
        //    {
        //        if (ctrEnem.GolpeIndio() == true)
        //        {
        //            energy = energy + premioEnemigo;
        //        }
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //SetControlEnemigo(collision.gameObject.GetComponent<ControlEnemigo>());
            ctrEnem = collision.gameObject.GetComponent<ControlEnemigo>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //golpes++;
        if (golpes == numGolpes)                
        {
            Destroy(collision.gameObject);
        }

    }


    //private void SetControlEnemigo(ControlEnemigo crt)
    //{
    //    ctrEnem = crt;
    //}

    void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
