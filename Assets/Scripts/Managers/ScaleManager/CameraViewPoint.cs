using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraViewportHandler : MonoBehaviour
{
    public enum Constraint
    {
        Landscape,
        Portrait
    }

    #region FIELDS

    public Color wireColor = Color.white;
    public float UnitsSize = 1; // size of your scene in unity units
    public Constraint constraint = Constraint.Portrait;
    public static CameraViewportHandler Instance;
    public new Camera camera;

    public bool executeInUpdate;

    //*** bottom screen

    //*** middle screen

    //*** top screen

    #endregion

    #region PROPERTIES

    public float Width { get; private set; }

    public float Height { get; private set; }

    // helper points:
    public Vector3 BottomLeft { get; private set; }

    public Vector3 BottomCenter { get; private set; }

    public Vector3 BottomRight { get; private set; }

    public Vector3 MiddleLeft { get; private set; }

    public Vector3 MiddleCenter { get; private set; }

    public Vector3 MiddleRight { get; private set; }

    public Vector3 TopLeft { get; private set; }

    public Vector3 TopCenter { get; private set; }

    public Vector3 TopRight { get; private set; }

    #endregion

    #region METHODS

    private void Awake()
    {
        camera = GetComponent<Camera>();
        Instance = this;
        ComputeResolution();
    }

    private void ComputeResolution()
    {
        float leftX, rightX, topY, bottomY;

        if (constraint == Constraint.Landscape)
            camera.orthographicSize = 1f / camera.aspect * UnitsSize / 2f;
        else
            camera.orthographicSize = UnitsSize / 2f;

        Height = 2f * camera.orthographicSize;
        Width = Height * camera.aspect;

        float cameraX, cameraY;
        cameraX = camera.transform.position.x;
        cameraY = camera.transform.position.y;

        leftX = cameraX - Width / 2;
        rightX = cameraX + Width / 2;
        topY = cameraY + Height / 2;
        bottomY = cameraY - Height / 2;

        //*** bottom
        BottomLeft = new Vector3(leftX, bottomY, 0);
        BottomCenter = new Vector3(cameraX, bottomY, 0);
        BottomRight = new Vector3(rightX, bottomY, 0);
        //*** middle
        MiddleLeft = new Vector3(leftX, cameraY, 0);
        MiddleCenter = new Vector3(cameraX, cameraY, 0);
        MiddleRight = new Vector3(rightX, cameraY, 0);
        //*** top
        TopLeft = new Vector3(leftX, topY, 0);
        TopCenter = new Vector3(cameraX, topY, 0);
        TopRight = new Vector3(rightX, topY, 0);
    }

    private void Update()
    {
#if UNITY_EDITOR

        if (executeInUpdate)
            ComputeResolution();

#endif
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = wireColor;

        var temp = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        if (camera.orthographic)
        {
            var spread = camera.farClipPlane - camera.nearClipPlane;
            var center = (camera.farClipPlane + camera.nearClipPlane) * 0.5f;
            Gizmos.DrawWireCube(new Vector3(0, 0, center),
                new Vector3(camera.orthographicSize * 2 * camera.aspect, camera.orthographicSize * 2, spread));
        }
        else
        {
            Gizmos.DrawFrustum(Vector3.zero, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane,
                camera.aspect);
        }

        Gizmos.matrix = temp;
    }

    #endregion
} // class