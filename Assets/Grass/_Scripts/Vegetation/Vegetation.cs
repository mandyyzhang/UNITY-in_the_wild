using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Renderer))]
public class Vegetation : MonoBehaviour
{
    [SerializeField] private float yOffset = 0f;

    private Collider coll;    
    private Renderer rend;
    private MaterialPropertyBlock block;
    private Matrix4x4 mat;

    private List<VegetationMover> affectors;
    public List<VegetationMover> Affectors
    {
        get { return affectors; }
    }

    private void Start()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
        rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
        mat = new Matrix4x4();
        block.SetFloat("_BaseYValue", transform.localPosition.y + yOffset);
        SetOffsets();
    }

    public void AddToAffectors(VegetationMover affector)
    {
        if(affectors == null)
        {
            affectors = new List<VegetationMover>();
        }
        if(!affectors.Contains(affector))
        {
            affectors.Add(affector);
        }
    }
    public void RemoveFromAffectors(VegetationMover affector)
    {
        if(affectors != null)
        {
            if(affectors.Contains(affector))
            {
                affectors.Remove(affector);
            }
        }
    }

    public void SetOffsets()
    {
        if (affectors == null)
        {
            mat.SetRow(0, Vector4.zero);
            mat.SetRow(1, Vector4.zero);
            mat.SetRow(2, Vector4.zero);
            mat.SetRow(3, Vector4.zero);
        }
        else if (affectors.Count == 0)
        {
            mat.SetRow(0, Vector4.zero);
            mat.SetRow(1, Vector4.zero);
            mat.SetRow(2, Vector4.zero);
            mat.SetRow(3, Vector4.zero);
        }
        else
        {
            int count = affectors.Count;
            if (count > 4)
            {
                count = 4;
                for (int i = 0; i < count; i++)
                {
                    if (affectors[i] != null)
                    {
                        Vector3 affectorPlusOffset = affectors[i].transform.position + affectors[i].Offset;
                        float y = affectorPlusOffset.y - transform.position.y;
                        float p = GetPush(affectors[i].Push, y);
                        Vector4 offsetter = new Vector4(affectorPlusOffset.x, affectorPlusOffset.y, affectorPlusOffset.z, p);

                        mat.SetRow(i, offsetter);
                    }
                    else
                    {
                        affectors.RemoveAt(i);
                        count = affectors.Count;
                    }
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (affectors[i] != null)
                    {
                        Vector3 affectorPlusOffset = affectors[i].transform.position + affectors[i].Offset;
                        float y = affectorPlusOffset.y - transform.position.y;
                        float p = GetPush(affectors[i].Push, y);
                        Vector4 offsetter = new Vector4(affectorPlusOffset.x, affectorPlusOffset.y, affectorPlusOffset.z, p);
                        
                        mat.SetRow(i, offsetter);
                    }
                    else
                    {
                        affectors.RemoveAt(i);
                        count = affectors.Count;
                    }
                }
                for (int i = count; i < 4; i++)
                {
                    mat.SetRow(i, Vector4.zero);
                }
            }
        }
        block.SetMatrix("_OffsetPositions4x4", mat);
        rend.SetPropertyBlock(block);
    }

    private float GetPush(float maxPush, float yDiff)
    {
        float d = maxPush;        

        if(yDiff < 0)
        {
            d += yDiff;
        }

        d = Mathf.Clamp(d, 0f, maxPush);

        return d;
    }
}