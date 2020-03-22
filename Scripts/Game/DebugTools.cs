using UnityEngine;

public static class DebugTools
{
    /*------------------------------------------------------------------*\
    |*							COMPLEXES
    \*------------------------------------------------------------------*/

    public static void DrawLineOfSight(Entity entity) {
        var color = entity.Awareness.Aggro.Count > 0 ? Color.yellow : Color.gray;
        DrawCircle(entity.Position, entity.Awareness.LigneOfSight, color);
    }

    public static void DrawRange(Entity entity) {
        var color = (entity.Engaging.Target && entity.Engaging.IsTargetInRange())
                ? Color.red
                : Color.yellow;

        DrawCircle(entity.Position, entity.Engaging.Range, color);
    }

    public static void DrawAggroList(Entity entity) {
        var aggro = entity.Awareness.Aggro;
        var closest = entity.Awareness.Closest;

        if (aggro.Count > 0) {
            foreach (var target in aggro) {
                var other = target.GetComponent<Entity>();
                var color = closest != null && other == closest ? Color.green : Color.yellow;
                var a = entity.Position;
                var b = other.Position;
                a.y = a.y + 1f;

                var direction = DirectionTo(a, b);
                var distance = direction.magnitude;

                DrawLine(a, b, color);
                DrawArrow(a, direction.normalized * (distance / 3), color);
            }
        }
    }

    public static void DrawDestination(Entity entity) {
        var origin = entity.Position;
        var destination = (Vector3) entity.Movable.Destination;
        DrawCircleWithLine(origin, destination, .3f, Selection.Instance.DestinationColor);
    }

    public static void DrawTarget(Entity entity) {
        var origin = entity.Position;
        var destination = entity.Engaging.Target.Position;
        origin.y += 1f;
        destination.y += 1f;
        DrawLine(origin, destination, Selection.Instance.TargetColor);
    }

    /*------------------------------------------------------------------*\
    |*							PRIMITIVES
    \*------------------------------------------------------------------*/

    public static Vector3 DirectionTo(Vector3 a, Vector3 b) {
        return b - a;
    }

    public static void DrawLine(Vector3 origin, Vector3 destination, Color color) {
        DebugExtension.DebugCylinder(origin, destination, color, .01f);
    }

    public static void DrawCylinder(Vector3 origin, Vector3 destination, Color color) {
        DebugExtension.DebugCylinder(origin, destination, color, .2f);
    }

    public static void DrawArrow(Vector3 origin, Vector3 direction, Color color) {
        DebugExtension.DebugArrow(origin, direction, color);
    }

    public static void DrawCircle(Vector3 origin, float radius, Color color) {
        DebugExtension.DebugCircle(origin, new Vector3(0, 1, 0), color, radius);
    }

    public static void DrawCircleWithLine(Vector3 origin, Vector3 destination, float radius,
                                           Color color) {
        var circleColor = color;
        DrawLine(origin, destination, circleColor);
        circleColor.a = circleColor.a * 2;
        DrawCircle(destination, radius, circleColor);
    }
}
