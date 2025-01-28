using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSettingManager : MonoBehaviour
{
    public bool isPhysicsEnabled = false;
    // Start is called before the first frame update
    private void Start()
    {
        Physics.autoSyncTransforms = isPhysicsEnabled;
    }
}
