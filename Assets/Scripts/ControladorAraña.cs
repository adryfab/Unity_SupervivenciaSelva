using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAraña : MonoBehaviour
{
    public float vel = -1f;

    Rigidbody2D rb;
    SpriteRenderer rend;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
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
