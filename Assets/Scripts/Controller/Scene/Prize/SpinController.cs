using System;
using UnityEngine;

public class SpinController : MonoBehaviour
{
    public bool Enabled { get; set; }

    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 60f, 0f);

    [SerializeField] private Space relativeTo = Space.World;

    void Update()
    {
        if (!Enabled)
            return;
        
        transform.Rotate(rotationSpeed * Time.deltaTime, relativeTo);
    }

    public void Awake()
    {
        Enabled = true;
    }

    public void Disable()
    {
        Enabled = false;
    }
}
