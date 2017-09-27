using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemigo : MonoBehaviour
{
    public int numGolpes = 3;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool GolpeIndio()
    {
        bool resp = false;
        numGolpes--;
        if (numGolpes <= 0)
        {
            resp = true;
            Destroy(gameObject);
        }
        return resp;
    }
}
