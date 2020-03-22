using UnityEngine;

public static class Ballistic
{
    private static float Gravity = Mathf.Abs(Physics.gravity.y);
    private static float AirTimeCoeff = 1f;


    private static Vector3 ComputeVelocity(Vector3 distance, float duration) {
        Vector3 distanceXZ = distance;
        distanceXZ.y = 1.3f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / duration;
        float Vy = Sy / duration + .5f * Mathf.Abs(Physics.gravity.y) * duration;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }


    public static Vector3 ComputeInitialVelocity(Vector3 origin, Vector3 target, float yOffset) {
        var distance = target - origin;
        distance.y = 0;
        target.y += yOffset;
        var time = (distance.magnitude * AirTimeCoeff) / (3 * Gravity);
        return ComputeVelocity(distance, time);
    }
}
