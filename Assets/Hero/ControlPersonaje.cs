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
    int golpes;
    bool enemy = false;
    GameObject enemigo;
    bool enFire = false;
    CircleCollider2D colider;

    void Start ()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        energy = 100;
    }

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Fire1")) > 0.01f)
        {
            if (enFire == false)
            {
                enFire = true;
                if (enemy == true)
                {
                    if (golpes >= numGolpes)
                    {
                        Destroy(enemigo);
                        golpes = 0;
                        energy += premioEnemigo;
                        if (energy > 100)
                        {
                            energy = 100;
                        }
                    }
                    colider = GetComponent<CircleCollider2D>();
                    colider.enabled = false;
                    enemy = false;
                    golpes++;
                }
                anim.SetTrigger("atacar");
            }
            else
            {
                enFire = false;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemy = true;
            enemigo = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy = false;
        enemigo = null;
    }

    void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    public void HabilitarTrigger()
    {
        colider = GetComponent<CircleCollider2D>();
        colider.enabled = true;
    }
}
