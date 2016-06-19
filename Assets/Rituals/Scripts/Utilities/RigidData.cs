using UnityEngine;
using System.Collections;

namespace RitualWarehouse
{
    public struct RigidData
    {
        public readonly float angularDrag;
        public readonly Vector2 centerOfMass;
        public readonly CollisionDetectionMode2D collisionDetectionMode;
        public readonly RigidbodyConstraints2D constraints;
        public readonly float drag;
        public readonly bool freezeRotation;
        public readonly float gravityScale;
        public readonly float inertia;
        public readonly RigidbodyInterpolation2D interpolation;
        public readonly bool isKinematic;
        public readonly float mass;
        public readonly bool simulated;

        public RigidData(Rigidbody2D originalData)
        {
            angularDrag = originalData.angularDrag;
            centerOfMass = originalData.centerOfMass;
            collisionDetectionMode = originalData.collisionDetectionMode;
            constraints = originalData.constraints;
            drag = originalData.drag;
            freezeRotation = originalData.freezeRotation;
            gravityScale = originalData.gravityScale;
            inertia = originalData.inertia;
            interpolation = originalData.interpolation;
            isKinematic = originalData.isKinematic;
            mass = originalData.mass;
            simulated = originalData.simulated;
        }

        public Rigidbody2D Assign(Rigidbody2D target)
        {
            target.angularDrag = angularDrag;
            target.centerOfMass = centerOfMass;
            target.collisionDetectionMode = collisionDetectionMode;
            target.constraints = constraints;
            target.drag = drag;
            target.freezeRotation = freezeRotation;
            target.gravityScale = gravityScale;
            target.inertia = inertia;
            target.interpolation = interpolation;
            target.isKinematic = isKinematic;
            target.mass = mass;
            target.simulated = simulated;

            return target;
        }

    }
}