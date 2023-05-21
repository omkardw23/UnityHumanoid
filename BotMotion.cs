using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class BotMotion : MonoBehaviour
{

    Animator animator;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();

        actions.Add("faster", Faster);
        actions.Add("slower", Slower);
        actions.Add("stop", Stop);
        actions.Add("jump",Jump);
        actions.Add("left",Left);
        actions.Add("right",Right);
       
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized +=  recognizedSpeech;
        keywordRecognizer.Start();
    }


    private void Left(){
        animator.SetTrigger("left");
    }

    private void Right(){
        animator.SetTrigger("right");
    }
    private void Jump(){
        animator.SetTrigger("jump");
    }

    private void Faster(){
        int currentSpeed = animator.GetInteger("speed");
        int newspeed = currentSpeed + 5;
        animator.SetInteger("speed",newspeed);
    }

    private void Slower(){
        int currentSpeed = animator.GetInteger("speed");
        int newspeed = currentSpeed - 5;
        if(newspeed <0) newspeed = 0;
        animator.SetInteger("speed",newspeed);
    }

    private void Stop(){
        animator.SetInteger("speed",0);
    }

    private void recognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
}
