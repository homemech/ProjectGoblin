using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminableObjectInfo : MonoBehaviour
{
    public StringValue objectTextInfo;
    public string examineObjectText;

    private void Start()
    {
        objectTextInfo.value = examineObjectText;
    }
}