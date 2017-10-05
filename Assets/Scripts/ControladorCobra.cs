using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCobra : MonoBehaviour
{
    public float vel = -1f;
    public float maxVel = 5f;
    public GameObject bulletPrototype;
    public int tiempoProyectil = 100;

    Rigidbody2D rb;
    bool emitidoProyectil = false;
    int cuentaProyectil;
    SpriteRenderer rend;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (emitidoProyectil == false && cuentaProyectil == tiempoProyectil)
        {
            EmitirProyectil();
            emitidoProyectil = true;
            cuentaProyectil = 0;
        }
        else
        {
            emitidoProyectil = false;
            cuentaProyectil++;
        }
    }

    void FixedUpdate()
    {
        Vector2 v = new Vector2(vel, 0);
        rb.velocity = v;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        vel *= -1;
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    public void EmitirProyectil()
    {
        GameObject bulletCopy = Instantiate(bulletPrototype);
        bulletCopy.transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        bulletCopy.GetComponent<ControlProyectil>().direction = new Vector3(transform.localScale.x * -1, 0, 0);
    }

    public void RecibirGolpe(int golpes)
    {
        switch (golpes)
        {
            case 1:
                rend.color = new Color(0, 0, 1, 1); //blue
                break;
            case 2:
                rend.color = new Color(0, 0, 0, 1); //black
                break;
            default:
                rend.color = new Color(1, 0, 0, 1); //red
                break;
        }
    }
}
