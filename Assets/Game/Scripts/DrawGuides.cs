using UnityEngine;
using Shapes;
using UnityEngine.Rendering;

[ExecuteAlways]
public class DrawGuides : ImmediateModeShapeDrawer
{
    [SerializeField] float radius = 0.01f;
    [SerializeField] Color color = Color.blue;
    [SerializeField] Color lineColor = Color.grey;
    [SerializeField] float lineThickness = 0.005f;
    [SerializeField] private HingeJoint joint;

    public override void DrawShapes(Camera cam)
    {
        if (cam == null) return;

        using (Draw.Command(cam))
        {
            Draw.ZTest = CompareFunction.Always;
            Vector3 center = transform.position;
            Vector3 normal = cam.transform.forward;
            if (joint != null)
            {
                Vector3 jointWorldAnchor = transform.TransformPoint(joint.anchor);
                Draw.Line(center, jointWorldAnchor, lineThickness, lineColor);
                Draw.Disc(jointWorldAnchor, normal, radius, color);
            }
            
            Draw.Disc(center, normal, radius, lineColor);
        }
    }
}