using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceController : MonoBehaviour
{

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();


    // Start is called before the first frame update
    void Start()
    {
        actions.Add("forward", Forward);
        actions.Add("up", Up);
        actions.Add("down", Down);
        actions.Add("back", Back);
        //actions.Add("left",Left);
        //actions.Add("right",Right);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized +=  recognizedSpeech;
        keywordRecognizer.Start();
    }

    private void Right()
    {
        StartCoroutine(MoveObject(transform.position + new Vector3(0, 0, 10), 1f)); // Move the object over 1 second
    }

    private void Left()
    {
        StartCoroutine(MoveObject(transform.position + new Vector3(0, 0, -10), 1f)); // Move the object over 1 second
    }

    // private void Back()
    // {
    //     transform.Translate(-10,0,0);
    // }

    private void Back()
    {
        StartCoroutine(MoveObject(transform.position + new Vector3(-10, 0, 0), 1f)); // Move the object over 1 second
    }

    private IEnumerator MoveObject(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // Calculate the interpolation parameter

            // Use Vector3.Lerp to smoothly move the object towards the target position
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        // Ensure the object reaches the exact target position
        transform.position = targetPosition;
    }



    private void Down()
    {
        StartCoroutine(MoveObject(transform.position + new Vector3(0, -10, 0), 1f)); // Move the object over 1 second
    }

    private void Up()
    {
        StartCoroutine(MoveObject(transform.position + new Vector3(0, 10, 0), 1f)); // Move the object over 1 second
    }

    private void Forward()
    {
        StartCoroutine(MoveObject(transform.position + new Vector3(10, 0, 0), 1f)); // Move the object over 1 second
    }

    private void recognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
