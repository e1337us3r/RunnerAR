using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimationSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = this.GetComponent<Animator>();

        animator.SetFloat("speed", Random.Range(0.5f, 2f));
    }
    // Test
    // Update is called once per frame
    void Update()
    {
        
    }
}
