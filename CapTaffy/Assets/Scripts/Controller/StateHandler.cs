using System.Collections;
using System.Collections.Generic;
using UnityEngine;




    public class StateHandler : MonoBehaviour
    {
        public float vertical;
        public float horizontal;

        public GameObject activeModel;
        public Animator anim;
        public Rigidbody rigid;

        public void Init()
        {
            //SetupAnimator();
            rigid = GetComponent<Rigidbody>();

        }

        /*
        void SetupAnimator()
        {
            if (activeModel == null)
            {
                anim = GetComponentInChilden<Animator>();
                if (anim == null)
                {
                    Debug.Log("No model found");
                }
                else
                {
                    activeModel = anim.gameObject;
                }
            }


            if (anim == null)
            anim = activeModel.GetComponent<Animator>();
        }

        anim.applyRootMotion = false;
        */
    }


