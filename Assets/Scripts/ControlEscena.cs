using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ControlEscena : MonoBehaviour {
    float version = 0.1f;

    // Use this for initialization
    void Start () {
        RegistrarInicio();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RegistrarInicio()
    {
        Debug.Log("RegistrarInicio");
        Analytics.CustomEvent("gameStart", new Dictionary<string, object> { });
    }

    public void RegistrarFin()
    {
        Debug.Log("RegistrarFin");
        float secsJuego = Time.time;
        float vidaPersonaje = GameObject.Find("Indio").GetComponent<ControlPersonaje>().energy;
        
        Analytics.CustomEvent("gameStart", new Dictionary<string, object>
        {
            { "time", secsJuego },
            { "vidaPersonaje", vidaPersonaje },
            { "version", version }
        });
    }
}
