using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsValidation : MonoBehaviour
{
    public DataType clientDataType;
    public bool IsRS232;

    private void Awake()
    {
        if (IsRS232)
        {
            transform.Find("IsRS_232").gameObject.SetActive(true);
        }
    }
}
