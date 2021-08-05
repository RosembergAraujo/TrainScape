using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    public void Next() {Dialogue_Controller.instance.NextSentence();}
}
