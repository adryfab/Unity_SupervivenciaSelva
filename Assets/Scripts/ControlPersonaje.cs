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
    public int dañoEnemigo = 1;
    public int premioObjeto = 10;
    public bool isOnTheFloor = false;
    public GameObject retroalimentacionEnergiaPrefab;

    Rigidbody2D rgb;
    Animator anim;
    bool haciaDerecha = true;
    int golpes;
    bool enemy = false;
    GameObject enemigo;
    bool enFire = false;
    CircleCollider2D colider;
    Transform retroalimentacionSpawnPoint;
    ControlEscena ctrEscena;

    void Start ()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        energy = 100;
        retroalimentacionSpawnPoint = GameObject.Find("spawnPoint").transform;
        ctrEscena = GameObject.Find("Escena").GetComponent<ControlEscena>();
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
                        Destroy(enemigo,1);
                        golpes = 0;
                        energy += premioEnemigo;
                        InstanciarRetroalimentacionEnergia(premioEnemigo);
                        if (energy > 100)
                        {
                            energy = 100;
                        }
                    }
                    if (enemigo.gameObject.name.Equals("Cobra"))
                    {
                        ControladorCobra ctr = enemigo.GetComponent<ControladorCobra>();
                        if (ctr != null)
                        {
                            ctr.RecibirGolpe(golpes);
                        }
                    } else if (enemigo.gameObject.name.Equals("Araña"))
                    {
                        ControladorAraña ctr = enemigo.GetComponent<ControladorAraña>();
                        if (ctr != null)
                        {
                            ctr.RecibirGolpe(golpes);
                        }
                    }
                    else if (enemigo.gameObject.name.Equals("Mono"))
                    {
                        ControladorMono ctr = enemigo.GetComponent<ControladorMono>();
                        if (ctr != null)
                        {
                            ctr.RecibirGolpe(golpes);
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
        if (energy <= 0)
        {
            anim.SetTrigger("morir");
            ctrEscena.RegistrarFin();
        }
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

        // Nueva variable agregada para corregir el error del salto infinito
        isOnTheFloor = rgb.velocity.y == 0;

        // ahora solo puede saltar si la velocidad del RGB es 0 en su componente 'y' 
        //es decir que no este cayendo o subiendo.
        if (Input.GetAxis("Jump") > 0 && isOnTheFloor)
        {
            rgb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            anim.SetTrigger("saltar");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemy = true;
            enemigo = collision.gameObject;
            energy = energy - dañoEnemigo;
            InstanciarRetroalimentacionEnergia(dañoEnemigo * -1);
            anim.SetTrigger("hit");
            if (energy < 0)
            {
                energy = 0;
            }
        }
        if (collision.tag == "Premio")
        {
            energy = energy + premioObjeto;
            InstanciarRetroalimentacionEnergia(premioObjeto);
            if (energy > 100)
            {
                energy = 100;
            }
            Destroy(collision.gameObject);
        }
    }

    public void RecibirDaño(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            energy = energy - dañoEnemigo;
            InstanciarRetroalimentacionEnergia(dañoEnemigo * -1);
            anim.SetTrigger("hit");
            if (energy < 0)
            {
                energy = 0;
            }
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

    private void InstanciarRetroalimentacionEnergia(int incremento)
    {
        GameObject retroalimentcionGO = null;
        if (retroalimentacionSpawnPoint != null)
        {
            retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, 
                retroalimentacionSpawnPoint.position, retroalimentacionSpawnPoint.rotation);
        }
        else
        {
            retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, 
                transform.position, transform.rotation);
        }

        retroalimentcionGO.GetComponent<RetroalimentacionEnergia>().cantidadCambiodeEnergia = incremento;
    }

}
