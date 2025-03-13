using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;

    const float G = 0.00667f;
    public static List<Gravity> gravityobjectlist;

    //orbit
    [SerializeField] bool planets = false;
    [SerializeField] int orbitSpeed = 1000;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gravityobjectlist == null)
        {
            gravityobjectlist = new List<Gravity>();
        }
         gravityobjectlist.Add(this);

        //orbit
        if (!planets)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }

    }


        private void FixedUpdate()
        {
            foreach (var obj in gravityobjectlist)
           {
            if (obj != this) 
                Attract(obj);
           }
        }

        void Attract(Gravity other)
        {
            Rigidbody otherRb = other.rb;
            Vector3 diraction = rb.position - otherRb.position;
            float distance = diraction.magnitude;

            float forceMagnitude = G * (rb.mass * otherRb.mass / Mathf.Pow(distance, 2));
            Vector3 gavityforce = forceMagnitude * diraction.normalized;

            otherRb.AddForce(gavityforce);

        }
    }

