using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class AnchorGameObject : MonoBehaviour
{
    public enum AnchorType
    {
        BottomLeft,
        BottomCenter,
        BottomRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        TopLeft,
        TopCenter,
        TopRight
    }

    public bool executeInUpdate;

    public AnchorType anchorType;
    public Vector3 anchorOffset;

    private IEnumerator updateAnchorRoutine; //Coroutine handle so we don't start it if it's already running

    // Use this for initialization
    private void Start()
    {
        updateAnchorRoutine = UpdateAnchorAsync();
        StartCoroutine(updateAnchorRoutine);
    }

#if UNITY_EDITOR
    // Update is called once per frame
    private void Update()
    {
        if (updateAnchorRoutine == null && executeInUpdate)
        {
            updateAnchorRoutine = UpdateAnchorAsync();
            StartCoroutine(updateAnchorRoutine);
        }
    }
#endif

    /// <summary>
    ///     Coroutine to update the anchor only once CameraFit.Instance is not null.
    /// </summary>
    private IEnumerator UpdateAnchorAsync()
    {
        uint cameraWaitCycles = 0;

        while (CameraViewportHandler.Instance == null)
        {
            ++cameraWaitCycles;
            yield return new WaitForEndOfFrame();
        }

        if (cameraWaitCycles > 0)
            print(string.Format("CameraAnchor found CameraFit instance after waiting {0} frame(s). " +
                                "You might want to check that CameraFit has an earlie execution order.",
                cameraWaitCycles));

        UpdateAnchor();
        updateAnchorRoutine = null;
    }

    private void UpdateAnchor()
    {
        switch (anchorType)
        {
            case AnchorType.BottomLeft:
                SetAnchor(CameraViewportHandler.Instance.BottomLeft);
                break;
            case AnchorType.BottomCenter:
                SetAnchor(CameraViewportHandler.Instance.BottomCenter);
                break;
            case AnchorType.BottomRight:
                SetAnchor(CameraViewportHandler.Instance.BottomRight);
                break;
            case AnchorType.MiddleLeft:
                SetAnchor(CameraViewportHandler.Instance.MiddleLeft);
                break;
            case AnchorType.MiddleCenter:
                SetAnchor(CameraViewportHandler.Instance.MiddleCenter);
                break;
            case AnchorType.MiddleRight:
                SetAnchor(CameraViewportHandler.Instance.MiddleRight);
                break;
            case AnchorType.TopLeft:
                SetAnchor(CameraViewportHandler.Instance.TopLeft);
                break;
            case AnchorType.TopCenter:
                SetAnchor(CameraViewportHandler.Instance.TopCenter);
                break;
            case AnchorType.TopRight:
                SetAnchor(CameraViewportHandler.Instance.TopRight);
                break;
        }
    }

    private void SetAnchor(Vector3 anchor)
    {
        var newPos = anchor + anchorOffset;
        if (!transform.position.Equals(newPos)) transform.position = newPos;
    }
}