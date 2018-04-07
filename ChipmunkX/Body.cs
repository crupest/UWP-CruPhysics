﻿using System;
using System.Collections.Generic;

using ChipmunkX.Native;
using ChipmunkX.Shapes;

namespace ChipmunkX
{
    public enum BodyType
    {
        Dynamic,
        Kinematic,
        Static
    }

    public static class BodyTypeNativeHelper
    {
        public static BodyType FromNative(cpBodyType type)
        {
            switch (type)
            {
                case cpBodyType.Dynamic:
                    return BodyType.Dynamic;
                case cpBodyType.Kinematic:
                    return BodyType.Kinematic;
                case cpBodyType.Static:
                    return BodyType.Static;
                default:
                    throw new ArgumentException("Unknown native body type.", nameof(type));
            }
        }

        public static cpBodyType ToNative(BodyType type)
        {
            switch (type)
            {
                case BodyType.Dynamic:
                    return cpBodyType.Dynamic;
                case BodyType.Kinematic:
                    return cpBodyType.Kinematic;
                case BodyType.Static:
                    return cpBodyType.Static;
                default:
                    throw new ArgumentException("Unknown body type.", nameof(type));
            }
        }
    }

    /// <summary>
    /// A body consisting of shapes.
    /// </summary>
    public class Body : ChipmunkObject
    {
        private List<Shape> _shapes = new List<Shape>();


        /// <summary>
        /// Create a body with no shape and specified type.
        /// </summary>
        /// <param name="bodyType">Body type.</param>
        public Body(BodyType bodyType = BodyType.Dynamic)
        {
            switch (bodyType)
            {
                case BodyType.Dynamic:
                    _ptr = BodyFuncs.cpBodyNew(0.0, 0.0);
                    break;
                case BodyType.Kinematic:
                    _ptr = BodyFuncs.cpBodyNewKinematic();
                    break;
                case BodyType.Static:
                    _ptr = BodyFuncs.cpBodyNewStatic();
                    break;
                default:
                    throw new ArgumentException("Unknown body type.", nameof(bodyType));
            }
        }


        /// <summary>
        /// Get or set the body type.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the body is not valid.
        /// </exception>
        public BodyType BodyType
        {
            get
            {
                CheckValidation();
                return BodyTypeNativeHelper.FromNative(BodyFuncs.cpBodyGetType(_ptr));
            }
            set
            {
                CheckValidation();
                BodyFuncs.cpBodySetType(_ptr, BodyTypeNativeHelper.ToNative(value));
            }
        }


        /// <summary>
        /// Get the list of shapes.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the body is not valid.
        /// </exception>
        public IReadOnlyList<Shape> Shapes
        {
            get
            {
                CheckValidation();
                return _shapes;
            }
        }

        public void AddShape(Shape shape)
        {
            throw new NotImplementedException();
        }

        public void RemoveShape(Shape shape)
        {
            throw new NotImplementedException();
        }

        protected override sealed void DoDispose()
        {
            throw new NotImplementedException();
        }
    }
}
