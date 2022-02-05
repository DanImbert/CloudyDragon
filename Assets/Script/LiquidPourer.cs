using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPourer : MonoBehaviour
{
    float lastEmit = 0;
    float emitInterval = .3f;
    public float pourVolume = 1f;
    public float pourForce = 1f;
    public LIquidContainer myContainer;
    ParticleSystem Particle;

    private void Awake()
    {
        //copyNode = LiquidEmitter.transform.GetChild(0);
        //liquidTrail = LiquidEmitter.GetComponent<TrailRenderer>();
        Particle = GetComponent<ParticleSystem>();
        UpdateLiquid();
       /* foreach (Transform node in LiquidEmitter.transform)
        {
            node.gameObject.SetActive(false);
        }*/
    }
  /*  public Transform PoolNode()
    {
        foreach (Transform node in LiquidEmitter.transform)
        {if (!node.gameObject.activeSelf)
            {
                return node;
            }
        }
        GameObject nNode = GameObject.Instantiate(copyNode.gameObject);
        nNode.transform.SetParent(LiquidEmitter.transform);
        return nNode.transform;
    }*/
    void FixedUpdate()
    {
        PourLiquid();
    }
    public void PourLiquid()
    {
        //liquidTrail.emitting = false;
        if (transform.up.y < 0)
        {
            float PourVolume = myContainer.SubstractVolume(pourVolume * Time.deltaTime);
           // liquidTrail.emitting = true;
            if (!Particle.isPlaying)
           Particle.Play();
            /*if (PourVolume > 0 && lastEmit<Time.time)
            {
                lastEmit = Time.time + emitInterval;
                Transform waterDrain = PoolNode();
                waterDrain.gameObject.SetActive(true);
                waterDrain.transform.position = transform.position + transform.up * transform.localScale.y;
                waterDrain.GetComponent<Rigidbody>().velocity = transform.up * pourForce;
                waterDrain.transform.SetAsLastSibling();
            }*/
        }else
        {

            Particle.Stop();
        }
    }
    public void UpdateLiquid()
    {
        ParticleSystem.TrailModule pSys = Particle.trails;
        pSys.colorOverTrail = new ParticleSystem.MinMaxGradient(myContainer.myLiquid.RimColor, myContainer.myLiquid.BodyColor);
    }
    private void Update()
    {
//UpdateTrail();
    }
    void UpdateTrail()
    {
        
      /*  if (liquidTrail.positionCount > 0)
        {
            liquidTrail.endColor = myContainer.myLiquid.RimColor;
            liquidTrail.startColor = myContainer.myLiquid.BodyColor;

            List<Vector3> positions = new List<Vector3>();
            for  (int N =0; N< LiquidEmitter.transform.childCount; N++)
            {
                Transform node = LiquidEmitter.transform.GetChild(N);
                if (node.gameObject.activeSelf)
                {
                    positions.Add(node.position);
                    
                }
                if (N==0)
                {
                    //  LiquidEmitter.transform.position = node.position;
                }
            }
            liquidTrail.SetPositions(positions.ToArray());
        }*/
    }
}
