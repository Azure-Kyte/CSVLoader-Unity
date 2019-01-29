using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{

    public List<GameObject> zones;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject g in zones)
        {
            if (Vector3.Distance(g.transform.position, Camera.main.transform.position) < 167f)
            {
                g.SetActive(true);
            }
            else
                g.SetActive(false);
        }
    }
}
