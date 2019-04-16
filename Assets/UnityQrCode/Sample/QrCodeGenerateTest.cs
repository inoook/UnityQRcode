using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QrCodeGenerateTest : MonoBehaviour
{
    [SerializeField] QrCodeGenerate qrCodeGenerate;
    [SerializeField] string qrCodeStr = "https://www.google.com/";
    
    Texture2D generatedQrCode;
    void OnGUI() {

        GUILayout.BeginArea(new Rect(10,10,300,300));
        GUILayout.Label(generatedQrCode);

        if (GUILayout.Button("Generate QRCode")) {
            generatedQrCode = qrCodeGenerate.GenerateQrCode(qrCodeStr);
        }
        GUILayout.EndArea();
    }
}
