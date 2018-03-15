using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatue : MonoBehaviour {

    [SerializeField]
    private GameObject TriggerLeft;
    [SerializeField]
    private GameObject TriggerRight;
    [SerializeField]
    private GameObject LeftChain;
    [SerializeField]
    private GameObject Effect;
    [SerializeField]
    private GameObject Door;
    [SerializeField]
    private GameObject RightChain;
    [SerializeField]
    private GameObject step;
    private bool TriggerLeftCheck;
    private bool TriggerRightCheck;
    private void Start()
    {
        TriggerLeftCheck=true;
        TriggerRightCheck=true;
    }

    private void Update()
    {
        
       
        if (TriggerRight == null && TriggerLeftCheck)
        {
            TriggerLeftCheck = false;
            step.SetActive(true);
            RightChain.layer = LayerMask.NameToLayer("IgnoreEveryThingButMainGround");
            foreach (Transform child in RightChain.transform)
            {
                if (null == child)
                {
                    continue;
                }
                child.gameObject.layer = LayerMask.NameToLayer("IgnoreEveryThingButMainGround");


            }

            //Destroy(RightChain, 20);
        }

        if (TriggerLeft == null && TriggerRightCheck)
        {
            TriggerRightCheck = false;
            LeftChain.layer = LayerMask.NameToLayer("IgnoreEveryThingButMainGround");
            foreach (Transform child in LeftChain.transform)
            {
                if (null == child)
                {
                    continue;
                }
                child.gameObject.layer = LayerMask.NameToLayer("IgnoreEveryThingButMainGround");


            }

            //Destroy(LeftChain, 20);
        }
        if (TriggerLeft==null && TriggerRight == null)
        {


            Effect.SetActive(true);
            Door.SetActive(true);

          
            //open the gate
        }





    }







}
