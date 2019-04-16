/*
* Copyright 2012 ZXing.Net authors
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System.Threading;

using UnityEngine;

using ZXing;
using ZXing.QrCode;

public class QrCodeWebCamRead : MonoBehaviour
{
    public delegate void QrCodeReadHandler(string result);
    public event QrCodeReadHandler eventQrCodeRead;

    private WebCamTexture camTexture;
    private Thread qrThread;

    private Color32[] textureColors;
    private int W, H;

    private Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);

    private bool isQuit;

    private string lastResult;

    void OnGUI() {
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
    }

    void OnEnable() {
        if (camTexture != null) {
            camTexture.Play();
            W = camTexture.width;
            H = camTexture.height;
        }
    }

    void OnDisable() {
        if (camTexture != null) {
            camTexture.Pause();
        }
    }

    void OnDestroy() {
        qrThread.Abort();
        camTexture.Stop();
    }

    // It's better to stop the thread by itself rather than abort it.
    void OnApplicationQuit() {
        isQuit = true;
    }

    void Start() {

        string deviceName = WebCamTexture.devices[0].name;
        Debug.Log(deviceName);
        camTexture = new WebCamTexture(deviceName);
        camTexture.requestedHeight = Screen.height; // 480;
        camTexture.requestedWidth = Screen.width; //640;
        OnEnable();

        qrThread = new Thread(DecodeQR);
        qrThread.Start();
    }

    void Update() {
        if (textureColors == null) {
            textureColors = camTexture.GetPixels32();
        }
    }

    void DecodeQR() {
        // create a reader with a custom luminance source
        //var barcodeReader = new BarcodeReader { AutoRotate = false, TryHarder = false };
        var barcodeReader = new BarcodeReader { AutoRotate = false };
        barcodeReader.Options.TryHarder = false;

        while (true) {
            if (isQuit)
                break;

            try {
                // decode the current frame
                var result = barcodeReader.Decode(textureColors, W, H);

                if (result != null) {
                    lastResult = result.Text;
                    //Debug.Log(result.Text);
                    if(eventQrCodeRead != null) {
                        eventQrCodeRead(lastResult);
                    }
                }

                // Sleep a little bit and set the signal to get the next frame
                Thread.Sleep(100);
                textureColors = null;
            }
            catch {
                //Debug.LogWarning("Error");
            }
        }
    }
}
