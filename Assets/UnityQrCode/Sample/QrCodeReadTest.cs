using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QrCodeReadTest : MonoBehaviour
{
    [SerializeField] QrCodeWebCamRead qrCodeRead;

    // Start is called before the first frame update
    void Start()
    {
        qrCodeRead.eventQrCodeRead += (result) => {
            Debug.LogWarning("result: "+result);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
