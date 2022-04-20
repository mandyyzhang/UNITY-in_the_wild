using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VegetationMover : MonoBehaviour
{
    [SerializeField] private float push = 1f;
    public float Push
    {
        get { return push; }
    }
    [SerializeField] private Vector3 offset = Vector3.zero;
    public Vector3 Offset
    {
        get { return offset; }
    }
    private Collider coll;

    private void Start()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Vegetation veg = other.GetComponent<Vegetation>();
        if(veg)
        {
            veg.AddToAffectors(this);
            veg.SetOffsets();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Vegetation veg = other.GetComponent<Vegetation>();
        if(veg)
        {
            if(veg.Affectors.Contains(this))
            {
                veg.SetOffsets();
            }
            else
            {
                veg.AddToAffectors(this);
                veg.SetOffsets();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Vegetation veg = other.GetComponent<Vegetation>();
        if(veg)
        {
            veg.RemoveFromAffectors(this);
            veg.SetOffsets();
        }
    }
}