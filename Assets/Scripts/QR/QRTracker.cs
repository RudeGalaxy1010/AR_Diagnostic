using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QRTracker : MonoBehaviour
{
    private const int SecondsForDetectingCamera = 60;
    private const int CameraTargetFPS = 30;

    [SerializeField] private RawImage _display;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;
    [SerializeField] private RectTransform _scanningZone;
    [SerializeField] private TMP_Text _text;

    private QRCodeScanner _scanner;
    private WebCamTexture _trackingTexture;
    private WebCamDevice _camera;
    private bool _isCameraFound = false;


    private IEnumerator Start()
    {
        int counter = 0;
        _scanner = new QRCodeScanner();

        while (_isCameraFound == false)
        {
            counter++;
            for (int i = 0; i < WebCamTexture.devices.Length; i++)
            {
                //if (WebCamTexture.devices[i].isFrontFacing == false)
                {
                    _camera = WebCamTexture.devices[i];
                    _isCameraFound = true;
                }
            }

            yield return new WaitForSeconds(1f);
            if (counter >= SecondsForDetectingCamera)
            {
                break;
            }
        }

        if (_isCameraFound)
        {
            SetupTexture(_camera);
            StartCoroutine(Scan());
        }
        else
        {
            Debug.LogError("Camera not found");
            _text.text = "Camera not found";
        }
    }

    private void SetupTexture(WebCamDevice camera)
    {
        _trackingTexture = new WebCamTexture(
            camera.name, 
            Convert.ToInt32(_scanningZone.rect.width), 
            Convert.ToInt32(_scanningZone.rect.height), 
            CameraTargetFPS);
        _display.texture = _trackingTexture;
        _trackingTexture.Play();
        UpdateCameraViewFit();
    }

    private void UpdateCameraViewFit()
    {
        float ratio = (float)_trackingTexture.width / _trackingTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientationAngle = -_trackingTexture.videoRotationAngle;
        _display.rectTransform.localEulerAngles = new Vector3(0, 0, orientationAngle);
    }

    private IEnumerator Scan()
    {
        while (true)
        {
            string decodedText = _scanner.TryScan(_trackingTexture);

            if (decodedText != "")
            {
                _text.text += decodedText + Environment.NewLine;
            }

            yield return new WaitForSeconds(0.15f);
        }
    }
}
