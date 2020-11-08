using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    public float FireRate;
    public float FireRange;
    public float HitForce;
    public int LazerDamage;
    private LineRenderer LazerLine;
    private bool LazerLineEnabled;
    private WaitForSeconds LazerDuration;
    private float NextFire;
    // Start is called before the first frame update
    void Start()
    {
        LazerLine = GetComponent<LineRenderer>();
    }
    void Fire()
    {
        Transform cam = Camera.main.transform;
        NextFire = Time.time + FireRate;
        Vector3 rayorigin = cam.position;
        LazerLine.SetPosition(0, transform.up * -10);
        RaycastHit hit;
        if (Physics.Raycast(rayorigin, cam.forward, out hit, FireRange))
        {
            LazerLine.SetPosition(1, hit.point);
            CubeBehaviour cubectrl = hit.collider.GetComponent<CubeBehaviour>();
            if (cubectrl != null)
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * HitForce);
                    cubectrl.Hit(LazerDamage);
                }
            }
        }
        else
        {
            LazerLine.SetPosition(1, cam.forward * FireRange);
        }
        StartCoroutine("LaserFX");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }
    private IEnumerator LaserFX()
    {
        LazerLine.enabled = true;
        yield return LazerDuration;
        LazerLine.enabled = false;
    }
}
