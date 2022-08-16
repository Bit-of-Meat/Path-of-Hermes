using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipLine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject zipPoint;
    public Material zipColor;
    public LayerMask surface;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50, surface))
            {
                //Making spawning the zipline objects
                GameObject zip1 = Instantiate(zipPoint, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
                GameObject zip2 = Instantiate(zipPoint, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                //Getting access to their child
                GameObject point1 = zip1.transform.GetChild(0).gameObject;
                GameObject point2 = zip2.transform.GetChild(0).gameObject;
                //Give them their own respective tags (VERY IMPORTANT)
                point1.tag = "Point1";
                point2.tag = "Point2";

                //Manual spaghetti code to add all the neccessary components
                point1.AddComponent<BoxCollider>();
                point1.AddComponent<LineRenderer>();
                point1.GetComponent<LineRenderer>().startWidth = 0.1f;
                point1.GetComponent<LineRenderer>().endWidth = 0.1f;
                point1.GetComponent<LineRenderer>().material = zipColor;
                point1.GetComponent<LineRenderer>().SetPosition(0, point1.transform.position);
                point1.GetComponent<LineRenderer>().SetPosition(1, point2.transform.position);
                point2.AddComponent<BoxCollider>();
                point2.AddComponent<LineRenderer>();
                point2.GetComponent<LineRenderer>().startWidth = 0.1f;
                point2.GetComponent<LineRenderer>().endWidth = 0.1f;
                point2.GetComponent<LineRenderer>().material = zipColor;
                point2.GetComponent<LineRenderer>().SetPosition(0, point2.transform.position);
                point2.GetComponent<LineRenderer>().SetPosition(1, point1.transform.position);
            }
        }
    }
}