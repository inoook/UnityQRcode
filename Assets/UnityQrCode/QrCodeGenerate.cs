// https://github.com/nenuadrian/qr-code-unity-3d-read-generate
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZXing;
using ZXing.QrCode;

public class QrCodeGenerate : MonoBehaviour
{

    private static Color32[] Encode(string textForEncoding, int width, int height, BarcodeFormat format = BarcodeFormat.QR_CODE, int margin = 2) {
        var writer = new BarcodeWriter {
            Format = format,
            Options = new QrCodeEncodingOptions {
                Height = height,
                Width = width
            }
        };
        writer.Options.Margin = margin;
        return writer.Write(textForEncoding);
    }

    public Texture2D GenerateQrCode(string text, int width = 256, int height = 256, BarcodeFormat format = BarcodeFormat.QR_CODE, int margin = 2) {
        var encoded = new Texture2D(width, height);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply(false, true);
        return encoded;
    }
}
