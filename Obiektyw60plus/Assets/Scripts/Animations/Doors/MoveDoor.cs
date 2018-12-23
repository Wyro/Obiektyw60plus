using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour {

    Animation animation;
    string AnimName;
    bool IsPlayingForward = false;
    bool AnimationStarted = false;

    // Use this for initialization
    void Start () {
        if (!animation) Debug.Log("no animation component");
        animation = GetComponent<Animation>();
        foreach (AnimationState state in animation) AnimName = state.name;
    }
	
	// Update is called once per frame
	void Update () {

        if (IsPlayingForward && !AnimationStarted)
        {
            animation[AnimName].time = 0;
            animation[AnimName].speed = 1;
            AnimationStarted = true;
        }
        else if (!IsPlayingForward && AnimationStarted)
        {
            animation[AnimName].time = animation[AnimName].length;
            animation[AnimName].speed = -1;
            AnimationStarted = false;
        }

        if (!animation.isPlaying)
        {
            if (Input.GetKeyDown("b"))
            {
                IsPlayingForward = !IsPlayingForward;
                animation.Play(AnimName);
            }
        }

	}
}
