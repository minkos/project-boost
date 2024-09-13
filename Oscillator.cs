using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    // [SerializeField] [Range(0, 1)] float movementFactor;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // smallest float: Mathf.Epsilon
        if (period <= Mathf.Epsilon) { return; }

        // to define the cycle with respect to time and period. If 10 seconds have elapsed, and there are 10 periods,...
        // ..then 1 cycle is defined.
        float cycles = Time.time / period;

        // 1 tau =  2 times of PI
        // 1 PI is the circumference of a circle
        const float tau = Mathf.PI * 2;

        // Mathf.Sin produces a number between 1 and -1 (for movementFactor)
        // cycles * tau: The input angle, in radians.
        float rawSineWave = Mathf.Sin(cycles * tau);

        // rawSineWave + 1f: range is 0 to 2.
        movementFactor = (rawSineWave + 1f) / 2; 

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset; 
    }
}
