using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : MonoBehaviour
{
    public abstract void Enter(T Onwer);
    public abstract void Update(T Onwer);
    public abstract void Exit(T Onwer);
}