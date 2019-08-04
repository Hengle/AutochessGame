using GridSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Movement : TileObjectProperties
{
    Vector3 _destination = Vector3.zero;
    DirectionEnum _direction;
    public float _rotationTime = 200f;

    private UnityEvent EndMove = new UnityEvent();
    private UnityEvent BeginMove = new UnityEvent();

#if UNITY_EDITOR

    [Header("DEBUG")]
    public bool debugMovement = false;
    public bool breakOnDebug = false;

    public bool alertOnStop = false;
    private static Vector3 stopLocation;
    private const float intervalStop = 0.2f; 

#endif

    // main implementation, move the object in unity space
    public IEnumerator MoveAdjacent(TileObject caller, float speed, DirectionEnum dir)
    {
        #region DEBUG
        #if UNITY_EDITOR
        if (debugMovement)
        {
            print("current tile: " + caller.tile);
            print("current location: " + caller.transform.position);
            print("caller null?: " + caller);
            print("Tile Locale Null?: " + caller.tile.locale);
            print("This is adjacent: " + (caller.tile.locale.Item1 + Direction.DirectionAsOffset(dir).Item1) + " , " + (caller.tile.locale.Item2 + Direction.DirectionAsOffset(dir).Item2));
            print("adjacent null?: " + caller?.tile?.GetAdjacent(dir));

            if (breakOnDebug)
                Debug.Break();
        }

        #endif
        #endregion

        // request to occupy tile in the specified direction.
        // does the adjacent tile exists?
        var check = caller?.tile?.GetAdjacent(dir);
        if (!check)
            yield break;
        // Is it available? occupy it
        if (!check.RequestOccupy(caller))
            yield break;

        OnBeginMove();

        // chache
        _direction = dir;
        var origin = new Vector3(caller.transform.position.x, 0, caller.transform.position.z);
        var delta = 0f;
        _destination = new Vector3(caller.CurrentIndex.Item1, 0, caller.CurrentIndex.Item2);

        // start turning
        StartCoroutine(Turn(caller, _rotationTime, _direction));

        // move
        while (caller.transform.position != _destination)
        {
            delta += speed * Time.deltaTime * ((_direction == DirectionEnum.North || _direction == DirectionEnum.South || _direction == DirectionEnum.West || _direction == DirectionEnum.East) ? 1 : 1 / Mathf.Sqrt(2));
            caller.transform.position = Vector3.Lerp(origin, _destination, delta);

            yield return null;
        }

        OnEndMove();
    }

    public IEnumerator MoveAdjacent(TileObject caller, float speed, DirectionEnum dir, Action callback)
    {
        yield return MoveAdjacent(caller, speed, dir);
        callback?.Invoke();
    }

    private IEnumerator Turn(TileObject obj, float speed, DirectionEnum dir)
    {
        var rot = Direction.DirectionAsDegree(dir);

        float counter = 0;

        Quaternion rotation = Quaternion.Euler(0, rot, 0);
        Quaternion origin = obj.transform.localRotation;

        while (counter < speed)
        {
            counter += (speed * Time.deltaTime);
            obj.transform.localRotation = Quaternion.RotateTowards(origin, rotation, counter);
            yield return null;
        }

    }

    private void OnBeginMove() => BeginMove?.Invoke();
    private void OnEndMove() => EndMove?.Invoke();
}

public static class Direction
{
    // Directions as degrees
    public const int n = 0, ne = 45, e = 90, se = 135, s = 180, sw = 225, w = 270, nw = 315, nn = 360;
    private static readonly int range = 45;

    public static DirectionEnum FromV2(Vector2 direction)
    {
        var angle = Vector2.Angle(Vector2.up, direction.normalized);

        return GetDirection(angle);
    }

    public static DirectionEnum GetDirection(float angle)
    {
        angle = angle % 360;

        angle = angle < 0 ? angle + 360 : angle;

        if (AngleWithinDirectionRange(angle, DirectionEnum.North))
            return DirectionEnum.North;
        if (AngleWithinDirectionRange(angle, DirectionEnum.NorthEast))
            return DirectionEnum.NorthEast;
        if (AngleWithinDirectionRange(angle, DirectionEnum.East))
            return DirectionEnum.East;
        if (AngleWithinDirectionRange(angle, DirectionEnum.SouthEast))
            return DirectionEnum.SouthEast;
        if (AngleWithinDirectionRange(angle, DirectionEnum.South))
            return DirectionEnum.South;
        if (AngleWithinDirectionRange(angle, DirectionEnum.SouthWest))
            return DirectionEnum.SouthWest;
        if (AngleWithinDirectionRange(angle, DirectionEnum.West))
            return DirectionEnum.West;
        if (AngleWithinDirectionRange(angle, DirectionEnum.NorthWest))
            return DirectionEnum.NorthWest;

        throw new FormatException("There's no Direction corresponding to the angel given motherfucker.");
    }

    public static Tuple<int, int> DirectionAsOffset(DirectionEnum dir)
    {
        switch (dir)
        {
            case DirectionEnum.North:
                return new Tuple<int, int>(0, 1);
            case DirectionEnum.East:
                return new Tuple<int, int>(1, 0);
            case DirectionEnum.South:
                return new Tuple<int, int>(0, -1);
            case DirectionEnum.West:
                return new Tuple<int, int>(-1, 0);
            case DirectionEnum.NorthEast:
                return new Tuple<int, int>(1, 1);
            case DirectionEnum.NorthWest:
                return new Tuple<int, int>(-1, 1);
            case DirectionEnum.SouthWest:
                return new Tuple<int, int>(-1, -1);
            case DirectionEnum.SouthEast:
                return new Tuple<int, int>(1, -1);
            default:
                return new Tuple<int, int>(0, 0);
        }
    }
    public static int DirectionAsDegree(DirectionEnum dir)
    {
        switch (dir)
        {
            case DirectionEnum.North:
                return n;
            case DirectionEnum.East:
                return e;
            case DirectionEnum.South:
                return s;
            case DirectionEnum.West:
                return w;
            case DirectionEnum.NorthEast:
                return ne;
            case DirectionEnum.NorthWest:
                return nw;
            case DirectionEnum.SouthWest:
                return sw;
            case DirectionEnum.SouthEast:
                return se;
            default:
                return n;
        }
    }

    private static bool AngleWithinDirectionRange(float angle, DirectionEnum dir)
    {
        if (angle < 0)
            angle += 360f;

        return angle > DirectionAsDegree(dir) - range/2f || angle < DirectionAsDegree(dir) + range/2f;
    }
}

[Flags]
public enum DirectionEnum
{
    North = 1,
    East = 2,
    South = 4,
    West = 8,
    NorthEast = North | East,
    NorthWest = North | West,
    SouthWest = South | West,
    SouthEast = South | East
}

