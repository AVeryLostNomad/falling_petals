using System;
using System.Collections;
using System.Collections.Generic;
using Action;
using UnityEngine;
using Random = UnityEngine.Random;

public class CeruleanController : MonoBehaviour
{

    public GameObject Pastel;
    public GameObject PebbleObject;
    private bool AlreadyShot = false;

    public String lastRunCode = "";

    private bool Cooldown = false;
    public float CooldownBetweenActions = 2.75f;
    private float _cooldown = 0f;

    public List<ActionBase> actions = new List<ActionBase>();
    public ActionBase CurrentAction;
    
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        actions.Add(new CeruleanFanSpin(this));   
        actions.Add(new CeruleanRightCorner(this));   
        actions.Add(new CeruleanLeftCorner(this));
        actions.Add(new CeruleanBuildWalls(this));
    }

    public GameObject MakePebble()
    {
        return Instantiate(PebbleObject);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cooldown)
        {
            _cooldown += Time.deltaTime;
            if (_cooldown > CooldownBetweenActions)
            {
                Cooldown = false;
                _cooldown = 0f;
                return;
            }
        }
        
        if (CurrentAction == null)
        {
            // TODO select an appropriate action for the occasion.
            List<ActionBase> applicableActions = new List<ActionBase>();
            foreach (ActionBase ab in actions)
            {
                if (ab.CanRun())
                {
                    applicableActions.Add(ab);
                }
            }

            int actionDecision = Random.Range(0, applicableActions.Count);
            CurrentAction = applicableActions[actionDecision];
            lastRunCode = CurrentAction.getCode();
            CurrentAction.Init();
            return;
        }

        CurrentAction.Update();
        if (CurrentAction.IsFinished())
        {
            CurrentAction = null;
            Cooldown = true;
        }
//        if (AlreadyShot) return;
//
    }
    
}
