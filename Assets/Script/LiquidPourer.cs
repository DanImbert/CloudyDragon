using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPourer : MonoBehaviour
{
    float PourInterval = 0;
    public float pourVolume = 1f;
    public float pourForce = 1f;
    LIquidContainer myContainer;
    public LiquidReciever Reciever;
    ParticleSystem Particle;

    private void Awake()
    {
        //copyNode = LiquidEmitter.transform.GetChild(0);
        //liquidTrail = LiquidEmitter.GetComponent<TrailRenderer>();
        myContainer = transform.parent.GetComponentInChildren<LIquidContainer>();
        Particle = GetComponent<ParticleSystem>();
        PourInterval = 1f / Particle.emission.rateOverTime.constant;

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
    private void OnEnable()
    {
        if (LiquidReciever.main!=null)
        {
            Particle.collision.AddPlane(LiquidReciever.main.transform);
        }
    }
    void FixedUpdate()
    {
        PourLiquid();
    }
    public void PourLiquid()
    {
        if (PourCoroutine == null)
        {
            PourCoroutine = StartCoroutine(PourLiquidCoroutine());
        }
    }
    Coroutine PourCoroutine;
    public IEnumerator PourLiquidCoroutine()
    {
        StartPouring();
        while (transform.up.y < 0 && myContainer.totalVolume > 0)
        {
            /*if (PourVolume > 0 && lastEmit<Time.time)
            {
                lastEmit = Time.time + emitInterval;
                Transform waterDrain = PoolNode();
                waterDrain.gameObject.SetActive(true);
                waterDrain.transform.position = transform.position + transform.up * transform.localScale.y;
                waterDrain.GetComponent<Rigidbody>().velocity = transform.up * pourForce;
                waterDrain.transform.SetAsLastSibling();
            }*/

            yield return new WaitForSeconds(PourInterval);
        }
        StopPouring();
    }

    public void StartPouring()
    {
        // liquidTrail.emitting = true;
        if (!Particle.isPlaying)
            Particle.Play();
    }
    public void StopPouring()
    {
        //liquidTrail.emitting = false;
        Particle.Stop();
        PourCoroutine = null;
    }
    public void UpdateLiquid()
    {
        ParticleSystem.TrailModule pSys = Particle.trails;
        pSys.colorOverTrail = new ParticleSystem.MinMaxGradient(myContainer.myLiquid.RimColor, myContainer.myLiquid.BodyColor);
    }/*
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
        }
        }*/
    public void TransferLiquid(LIquidContainer otherC)
    {
        myContainer.TransferLiquid(otherC, pourVolume * PourInterval,true);
    }
}
