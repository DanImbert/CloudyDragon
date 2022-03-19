using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPourer : MonoBehaviour
{
    float PourInterval = 0;
    public float pourVolume = 1f;
    public float pourForce = 1f;
    LiquidHolder myContainer;
    ParticleSystem Particle;
    public LiquidReciever targetReciever;
    public AudioSource PourSound;
    private void Awake()
    {
        //copyNode = LiquidEmitter.transform.GetChild(0);
        //liquidTrail = LiquidEmitter.GetComponent<TrailRenderer>();
        myContainer = transform.parent.GetComponentInChildren<LiquidHolder>();
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
    protected void OnEnable()
    {
        ChangeTarget(targetReciever);
    }
    public void ChangeTarget(LiquidReciever nTarget)
    {
        if (nTarget != null )
        {
            targetReciever = nTarget;
            Particle.collision.SetPlane(0,targetReciever.transform);
        }
    }
    protected void FixedUpdate()
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
    protected IEnumerator PourLiquidCoroutine()
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
            if (!Particle.isPlaying)
            {
                Particle.Play();
                //TODO Audio: Start Pouring Liquid
                PourSound.Play();
            }
            StartCoroutine(TransferLiquid(targetReciever.Container));
            yield return new WaitForSeconds(PourInterval);
        }
        StopPouring();
    }

    protected void StartPouring()
    {
        // liquidTrail.emitting = true;
        UpdateLiquid();
    }
    public void StopPouring()
    {
        if (PourCoroutine != null)
            PourSound.Stop();
        StopCoroutine(PourCoroutine);
        //liquidTrail.emitting = false;
        Particle.Stop();
        //TODO Audio: Stop Pouring Liquid
        
        PourCoroutine = null;
    }
    protected void UpdateLiquid()
    {
        ParticleSystem.TrailModule pSys = Particle.trails;
        pSys.colorOverTrail = new ParticleSystem.MinMaxGradient(myContainer.RimColor, myContainer.BodyColor);
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
    protected IEnumerator TransferLiquid(LiquidHolder otherC)
    {
        yield return new WaitForSeconds(.3f);
        myContainer.TransferLiquid(otherC, pourVolume * PourInterval,true);
        if (otherC.GetFillPercent() >= 1 && GameController.main.state < GameController.GameState.shaker)
            GameController.main.GoToNextPhase();
    }
    protected void OnDisable()
    {
        StopPouring();
        
    }
}
