using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlide : MonoBehaviour
{
    public Animator transition;

    // Start is called before the first frame update
    public void NextPage()
    {
        transition.SetTrigger("Next");
    }

    public void PrevPage()
    {
        transition.SetTrigger("Previous");
    }
}
