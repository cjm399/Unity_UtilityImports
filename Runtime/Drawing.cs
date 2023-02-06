using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpacePigs.Drawing
{
    public static class DrawDebug
    {
        public static void Arrow(Ray _ray, Color? _col = null, float _width = 5f, float _arrowHeadLength = 0.25f, float _arrowHeadAngle = 20.0f)
        {
            Color col = _col.GetValueOrDefault(Color.red);
            Ray(_ray.origin, _ray.direction, col, _width);

            Vector3 right = Quaternion.LookRotation(_ray.direction) * Quaternion.Euler(0, 180 + _arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(_ray.direction) * Quaternion.Euler(0, 180 - _arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Ray(_ray.origin + _ray.direction, right * _arrowHeadLength, col, _width);
            Ray(_ray.origin + _ray.direction, left * _arrowHeadLength, col, _width);
        }

        public static void Arrow(Vector3 _origin, Vector3 _end, Color? _col = null, float _width = 5f, float _arrowHeadLength = 0.25f, float _arrowHeadAngle = 20.0f)
        {
            Color col = _col.GetValueOrDefault(Color.red);
            Line(_origin, _end, col, _width);
            Vector3 direction = (_end - _origin).normalized;
            Vector3 lRight = Vector3.Cross(direction, Vector3.up);
            Vector3 lUp = Vector3.Cross(direction, lRight);
            lRight.Normalize();
            lUp.Normalize();

            float y = Mathf.Sin(_arrowHeadAngle * Mathf.Deg2Rad) * _arrowHeadLength;
            float x = Mathf.Cos(_arrowHeadAngle * Mathf.Deg2Rad) * _arrowHeadLength;
            Vector3 back = -direction * x;
            Vector3 up = lUp * y;
            Vector3 right = lRight * y;

            Vector3 downV = back - up;
            Vector3 upV = back + up;
            Vector3 rightV = back + right;
            Vector3 leftV = back - right;

            Line(_end, _end + rightV.normalized * _arrowHeadLength, col, _width);
            Line(_end, _end + leftV.normalized * _arrowHeadLength, col, _width);
            Line(_end, _end + upV.normalized * _arrowHeadLength, col, _width);
            Line(_end, _end + downV.normalized * _arrowHeadLength, col, _width);
        }

        public static void Line(Vector3 _origin, Vector3 _dest, Color? _col = null, float _width = 1)
        {
            Color col = _col.GetValueOrDefault(Color.red);
            int count = 1 + Mathf.CeilToInt(_width); // how many lines are needed.
            if (count == 1)
            {
                Debug.DrawLine(_origin, _dest, col);
            }
            else
            {
                Camera c = Camera.current;
                if (c == null)
                {
                    Debug.LogError("Camera.current is null");
                    return;
                }
                var scp1 = c.WorldToScreenPoint(_origin);
                var scp2 = c.WorldToScreenPoint(_dest);

                Vector3 v1 = (scp2 - scp1).normalized; // line direction
                Vector3 n = Vector3.Cross(v1, Vector3.forward); // normal vector

                for (int i = 0; i < count; i++)
                {
                    Vector3 o = 0.99f * n * _width * ((float)i / (count - 1) - 0.5f);
                    Vector3 origin = c.ScreenToWorldPoint(scp1 + o);
                    Vector3 destiny = c.ScreenToWorldPoint(scp2 + o);
                    Debug.DrawLine(origin, destiny, col);
                }
            }
        }

        public static void Ray(Vector3 _origin, Vector3 _direction, Color? _col = null, float _width = 1)
        {
            Color col = _col.GetValueOrDefault(Color.red);
            int count = 1 + Mathf.CeilToInt(_width); // how many lines are needed.
            if (count == 1)
            {
                Debug.DrawRay(_origin, _direction, col);
            }
            else
            {
                Camera c = Camera.current;
                if (c == null)
                {
                    Debug.LogError("Camera.current is null");
                    return;
                }
                var scp1 = c.WorldToScreenPoint(_origin);
                var scp2 = c.WorldToScreenPoint(_origin + _direction);

                Vector3 v1 = (scp2 - scp1).normalized; // line direction
                Vector3 n = Vector3.Cross(v1, Vector3.forward); // normal vector

                for (int i = 0; i < count; i++)
                {
                    Vector3 o = 0.99f * n * _width * ((float)i / (count - 1) - 0.5f);
                    Vector3 origin = c.ScreenToWorldPoint(scp1 + o);
                    Vector3 destiny = c.ScreenToWorldPoint(scp2 + o);
                    Debug.DrawLine(origin, destiny, col);
                }
            }
        }
    }

    public static class DrawGizmo
    {
        public static void Arrow(Ray _ray, float _width = 1, float _arrowHeadLength = 0.25f, float _arrowHeadAngle = 20.0f)
        {
            Ray(_ray.origin, _ray.direction, _width);

            Vector3 right = Quaternion.LookRotation(_ray.direction) * Quaternion.Euler(0, 180 + _arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(_ray.direction) * Quaternion.Euler(0, 180 - _arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Ray(_ray.origin + _ray.direction, right * _arrowHeadLength);
            Ray(_ray.origin + _ray.direction, left * _arrowHeadLength);
        }

        public static void Arrow(Vector3 _origin, Vector3 _end, float _width = 5f, float _arrowHeadLength = 0.25f, float _arrowHeadAngle = 20.0f)
        {
            Line(_origin, _end, _width);
            Vector3 direction = (_end - _origin).normalized;
            Vector3 lRight = Vector3.Cross(direction, Vector3.up);
            Vector3 lUp = Vector3.Cross(direction, lRight);
            lRight.Normalize();
            lUp.Normalize();

            float y = Mathf.Sin(_arrowHeadAngle * Mathf.Deg2Rad) * _arrowHeadLength;
            float x = Mathf.Cos(_arrowHeadAngle * Mathf.Deg2Rad) * _arrowHeadLength;
            Vector3 back = -direction * x;
            Vector3 up = lUp * y;
            Vector3 right = lRight * y;

            Vector3 downV = back - up;
            Vector3 upV = back + up;
            Vector3 rightV = back + right;
            Vector3 leftV = back - right;

            Line(_end, _end + rightV.normalized * _arrowHeadLength, _width);
            Line(_end, _end + leftV.normalized * _arrowHeadLength, _width);
            Line(_end, _end + upV.normalized * _arrowHeadLength, _width);
            Line(_end, _end + downV.normalized * _arrowHeadLength, _width);
        }

        public static void Line(Vector3 _origin, Vector3 _dest, float _width = 1)
        {
            int count = 1 + Mathf.CeilToInt(_width); // how many lines are needed.
            if (count == 1)
            {
                Gizmos.DrawLine(_origin, _dest);
            }
            else
            {
                Camera c = Camera.current;
                if (c == null)
                {
                    Debug.LogError("Camera.current is null");
                    return;
                }
                var scp1 = c.WorldToScreenPoint(_origin);
                var scp2 = c.WorldToScreenPoint(_dest);

                Vector3 v1 = (scp2 - scp1).normalized; // line direction
                Vector3 n = Vector3.Cross(v1, Vector3.forward); // normal vector

                for (int i = 0; i < count; i++)
                {
                    Vector3 o = 0.99f * n * _width * ((float)i / (count - 1) - 0.5f);
                    Vector3 origin = c.ScreenToWorldPoint(scp1 + o);
                    Vector3 destiny = c.ScreenToWorldPoint(scp2 + o);
                    Gizmos.DrawLine(origin, destiny);
                }
            }
        }

        public static void Ray(Vector3 _origin, Vector3 _direction, float _width = 1)
        {
            int count = 1 + Mathf.CeilToInt(_width); // how many lines are needed.
            if (count == 1)
            {
                Gizmos.DrawRay(_origin, _direction);
            }
            else
            {
                Camera c = Camera.current;
                if (c == null)
                {
                    Debug.LogError("Camera.current is null");
                    return;
                }
                var scp1 = c.WorldToScreenPoint(_origin);
                var scp2 = c.WorldToScreenPoint(_origin + _direction);

                Vector3 v1 = (scp2 - scp1).normalized; // line direction
                Vector3 n = Vector3.Cross(v1, Vector3.forward); // normal vector

                for (int i = 0; i < count; i++)
                {
                    Vector3 o = 0.99f * n * _width * ((float)i / (count - 1) - 0.5f);
                    Vector3 origin = c.ScreenToWorldPoint(scp1 + o);
                    Vector3 destiny = c.ScreenToWorldPoint(scp2 + o);
                    Gizmos.DrawLine(origin, destiny);
                }
            }
        }
    }
}