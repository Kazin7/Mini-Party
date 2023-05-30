using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowItemObject : MonoBehaviour
{
    private bool _isComplete;
    public bool isComplete => _isComplete;

    private void Start()
    {
        StartCoroutine(ChangeValue());
    }
    IEnumerator ChangeValue()
    {
        yield return new WaitForSeconds(1);
        _isComplete = true;
    }
}
