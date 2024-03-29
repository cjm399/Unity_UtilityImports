﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Not currently used. Will I ever use this?
public struct lerpSettings
{
    public float time;
    public float a;
    public float b;
}
namespace SpacePigs.Utilities
{
    public static class UIHelpers
    {
        #region Initialization

        public class MyStaticMB : MonoBehaviour { }
        private static MyStaticMB myStaticMB;
        private static MyStaticMB moverMB;

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            if (myStaticMB == null)
            {
                GameObject gameObject = new GameObject("Static_UI_Coroutines");
                myStaticMB = gameObject.AddComponent<MyStaticMB>();
                moverMB = gameObject.AddComponent<MyStaticMB>();
                Object.DontDestroyOnLoad(gameObject);
            }
        }

        #endregion

        #region WorldToScreenSpace

        public static void WorldSpaceToScreenSpace(
            ref Transform _trans, ref RectTransform _rectTrans,
            ref Canvas _canvas, ref Camera _cam,
            float xLeftPadding = 0, float xRightPadding = 0,
            float yTopPadding = 0, float yBottomPadding = 0
            )
        {
            float width = _rectTrans.rect.xMax - _rectTrans.rect.xMin;
            float height = _rectTrans.rect.yMax - _rectTrans.rect.yMin;

            Vector3 screenPos = _cam.WorldToViewportPoint(_trans.TransformPoint(Vector3.zero));

            screenPos.x = Mathf.Clamp01(screenPos.x);
            screenPos.y = Mathf.Clamp01(screenPos.y);
            _rectTrans.anchorMin = screenPos;
            _rectTrans.anchorMax = new Vector2(.5f + (screenPos.x - .5f), .5f + (screenPos.y - .5f));
            _rectTrans.anchoredPosition = Vector2.zero;

            int canvasWidth = (int)(_canvas.pixelRect.xMax / _canvas.scaleFactor);
            int canvasHeight = (int)(_canvas.pixelRect.yMax / _canvas.scaleFactor);

            Vector2 uiExtents = new Vector2(width / canvasWidth, height / canvasHeight) / 2;

            screenPos = _cam.WorldToViewportPoint(_trans.TransformPoint(Vector3.zero));

            if (screenPos.z < 0)
            {
                screenPos.y *= -1;
            }

            screenPos.x = Mathf.Clamp(screenPos.x, uiExtents.x + xLeftPadding, 1 - uiExtents.x - xRightPadding);
            screenPos.y = Mathf.Clamp(screenPos.y, uiExtents.y + yBottomPadding, 1 - uiExtents.y - yTopPadding);

            if (screenPos.z < 0)
            {
                screenPos.x = 1 - screenPos.x;
                screenPos.x = Mathf.Clamp(screenPos.x, uiExtents.x, 1 - uiExtents.x);
            }

            _rectTrans.anchorMin = screenPos;
            _rectTrans.anchorMax = screenPos;
        }

        #endregion

        #region Radial Positioning

        public static Vector2[] PositionRadially(float _distributionAngle, float _offset, float _distributionRadius, ref RectTransform[] _objsToDistribute, bool _shouldSetPositions = false)
        {
            Vector2[] returnPositions = GetRadialPositions(_distributionAngle, _offset, _distributionRadius, _objsToDistribute.Length);

            if (_shouldSetPositions)
            {
                for (int posIndex = 0; posIndex < _objsToDistribute.Length; ++posIndex)
                {
                    _objsToDistribute[posIndex].anchoredPosition = returnPositions[posIndex];
                }
            }

            return returnPositions;
        }

        public static Vector2[] PositionRadially(float _distributionAngle, float _offset, float _distributionRadius, ref List<RectTransform> _objsToDistribute, bool _shouldSetPositions = false)
        {
            Vector2[] returnPositions = GetRadialPositions(_distributionAngle, _offset, _distributionRadius, _objsToDistribute.Count);

            if (_shouldSetPositions)
            {
                for (int posIndex = 0; posIndex < _objsToDistribute.Count; ++posIndex)
                {
                    _objsToDistribute[posIndex].anchoredPosition = returnPositions[posIndex];
                }
            }

            return returnPositions;
        }

        public static Vector2[] GetRadialPositions(float _distributionAngle, float _offset, float _distributionRadius, int _objsToDistribute)
        {
            Vector2[] returnPositions = new Vector2[_objsToDistribute];

            Debug.Assert(_distributionAngle <= 360f, "Cannot have a distribution angle larger than 360.");
            Debug.Assert(_objsToDistribute > 0, "You need at least 1 object to distribute.");

            if (_distributionAngle == 360f)
                _distributionAngle = 360f - (360f / _objsToDistribute);

            float halfDistribution = _distributionAngle / 2.0f;

            if (_objsToDistribute != 1)
            {

                for (int index = 0; index < _objsToDistribute; ++index)
                {
                    float d = (float)index / (float)(_objsToDistribute - 1) * _distributionAngle;
                    float xRatio = Mathf.Cos((d + _offset - halfDistribution) * Mathf.Deg2Rad);
                    float yRatio = Mathf.Sin((d + _offset - halfDistribution) * Mathf.Deg2Rad);

                    returnPositions[index] = new Vector2(-_distributionRadius * xRatio, _distributionRadius * yRatio);
                }
            }
            else
            {
                float xRatio = Mathf.Cos((_offset) * Mathf.Deg2Rad);
                float yRatio = Mathf.Sin((_offset) * Mathf.Deg2Rad);

                returnPositions[0] = new Vector2(-_distributionRadius * xRatio, _distributionRadius * yRatio);
            }

            return returnPositions;
        }

        #endregion

        #region ScreenSizing/Positioning

        [System.Diagnostics.Contracts.Pure]
        public static float CalculateElementUniformScale(Rect _drawRect, float _elementPadding, int _columns, int _rows)
        {
            float screenWidth = _drawRect.width;
            float screenHeight = _drawRect.height;

            float paddingPixelSize = _elementPadding;

            float letterScale = Mathf.Min(
                (screenWidth - (paddingPixelSize * _columns)) / _columns,
                (screenHeight - (paddingPixelSize * _rows)) / _rows);

            return letterScale;
        }

        #endregion

        #region Canvas

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static bool isHoveringUIElement()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
            };

            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            bool result = false;
            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; ++i)
                {
                    result |= results[0].gameObject.layer == 5; // 5 is Unity's UI layer
                }
            }

            return result;
        }

        #region ToggleCanvas Group

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void ToggleCanvasGroup(ref CanvasGroup _cg, bool _targetValue = true)
        {
            _cg.alpha = Static.BoolAsPercent(_targetValue);
            _cg.blocksRaycasts = _targetValue;
            _cg.interactable = _targetValue;
        }

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void ToggleCanvasGroup(ref CanvasGroup _cg)
        {
            bool isVisible = !Static.PercentAsBool(_cg.alpha);
            _cg.alpha = Static.BoolAsPercent(isVisible);
            _cg.blocksRaycasts = isVisible;
            _cg.interactable = isVisible;
        }

        #endregion

        #endregion

        #region MoveTo

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void MoveTo(ref RectTransform _trans, Vector2 _location, Vector2 _endScale, float _moveSpeed, AnimationCurve _speedOverTime)
        {
            moverMB.StartCoroutine(MoveTo_Internal(_trans, _location, _endScale, _moveSpeed, _speedOverTime));
        }

        private static IEnumerator MoveTo_Internal(RectTransform _trans, Vector2 _location, Vector2 _endScale, float _moveSpeed, AnimationCurve _speedOverTime)
        {
            //speed = distance / time =>
            // time = distance / speed;
            Vector2 startPos = _trans.position;
            Vector2 startScale = _trans.localScale;
            float distance = Vector2.Distance(startPos, _location);
            float time = distance / _moveSpeed;
            float currTime = 0;
            float percent = 0;
            while (percent < 1)
            {
                percent = (currTime / time);
                _trans.position = Vector2.Lerp(startPos, _location, _speedOverTime.Evaluate(percent));
                _trans.localScale = Vector2.Lerp(startScale, _endScale, _speedOverTime.Evaluate(percent));
                currTime += Time.deltaTime;
                yield return null;
            }
        }

        public static void AbsoluteMoveTo(ref RectTransform _rectTrans, ref AnimationCurve _animCurve, float _speed, Vector3 _destination)
        {
            Vector3 displacement = _destination - _rectTrans.position;
            myStaticMB.StartCoroutine(Internal_UITranslate(_rectTrans, _animCurve, _speed, displacement));
        }

        public static void LocalMoveTo(ref RectTransform _rectTrans, ref AnimationCurve _animCurve, float _speed, Vector3 _destination)
        {
            Vector3 displacement = _destination - _rectTrans.localPosition;
            myStaticMB.StartCoroutine(Internal_UITranslate(_rectTrans, _animCurve, _speed, displacement));
        }

        public static void UITranslate(ref RectTransform _rectTrans, ref AnimationCurve _animCurve, float _speed, Vector3 _translationOffset)
        {
            myStaticMB.StartCoroutine(Internal_UITranslate(_rectTrans, _animCurve, _speed, _translationOffset));
        }

        private static IEnumerator Internal_UITranslate(RectTransform _rectTrans, AnimationCurve _animCurve, float _speed, Vector3 _translationOffset)
        {
            Vector3 startPos = _rectTrans.localPosition;
            float distance = _translationOffset.magnitude;
            float totalLerpTime = distance / _speed;
            float timer = 0;
            float percentage = 0;

            while (percentage < 1)
            {
                percentage = timer / totalLerpTime;
                _rectTrans.localPosition = startPos + (_translationOffset * (_animCurve.Evaluate(percentage)));
                timer += Time.deltaTime;
                yield return null;
            }
        }

        public static void UIJump(ref RectTransform _rectTrans, ref AnimationCurve _animCurve, float _speed, float _height)
        {
            myStaticMB.StartCoroutine(Internal_UIJump(_rectTrans, _animCurve, _speed, _height));
        }

        private static IEnumerator Internal_UIJump(RectTransform _rectTrans, AnimationCurve _animCurve, float _speed, float _height)
        {
            // speed = distance / time;
            //time = distance / speed;
            float totalLerpTime = _height * 2 / _speed;
            float timer = 0;
            float percentage = 0;
            Vector3 startPos = _rectTrans.localPosition;

            while (percentage < 1)
            {
                percentage = timer / totalLerpTime;
                _rectTrans.localPosition = startPos + (Vector3.up * (_animCurve.Evaluate(percentage) * _height));
                timer += Time.deltaTime;
                yield return null;
            }
        }

        public static void UIRotate(ref RectTransform _rectTrans, ref AnimationCurve _animCurve, float _speed, float _angle)
        {
            myStaticMB.StartCoroutine(Internal_UIRotate(_rectTrans, _animCurve, _speed, _angle));
        }

        private static IEnumerator Internal_UIRotate(RectTransform _rectTrans, AnimationCurve _animCurve, float _speed, float _angle)
        {
            // speed = distance / time;
            //time = distance / speed;
            float totalLerpTime = _angle * 2 / _speed;
            float timer = 0;
            float percentage = 0;
            Quaternion startRot = _rectTrans.rotation;

            while (percentage < 1)
            {
                percentage = timer / totalLerpTime;
                float newAngle = startRot.eulerAngles.z + (_animCurve.Evaluate(percentage) * _angle);
                _rectTrans.rotation = Quaternion.Euler(0, 0, newAngle);//Rotate(0, 0, _animCurve.Evaluate(percentage) * _angle * Time.deltaTime);
                //_rectTrans.rotation = startPos + (Vector3.up * (_animCurve.Evaluate(percentage) * _angle));
                timer += Time.deltaTime;
                yield return null;
            }
        }


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void StopAllMovementCoroutines()
        {
            moverMB.StopAllCoroutines();
        }
        
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void StopAllMoveOperations()
        {
            moverMB.StopAllCoroutines();
        }
        #endregion

        #region Lerping

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void LerpImageColor(ref UnityEngine.UI.Image _img, Color _start, Color _end, float _time)
        {
            myStaticMB.StartCoroutine(Internal_LerpImageColor(_img, _start, _end, _time));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void LerpImageColor(ref UnityEngine.UI.Image _img, ref AnimationCurve _animCurve, Color _start, Color _end, float _time)
        {
            myStaticMB.StartCoroutine(Internal_LerpImageColor(_img, _animCurve, _start, _end, _time));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void LerpImageOpacity(ref UnityEngine.UI.Image _img, float _start, float _end, float _time)
        {
            Color start = _img.color;
            Color end = start;
            start.a = _start;
            end.a = _end;
            myStaticMB.StartCoroutine(Internal_LerpImageColor(_img, start, end, _time));
        }

        private static IEnumerator Internal_LerpImageColor(UnityEngine.UI.Image _img, Color _start, Color _end, float _lerpTime)
        {
            float timer = 0;
            float percentage = 0;

            while (percentage < 1)
            {
                percentage = (timer / _lerpTime);
                _img.color = Color.Lerp(_start, _end, percentage);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        private static IEnumerator Internal_LerpImageColor(UnityEngine.UI.Image _img, AnimationCurve _animCurve, Color _start, Color _end, float _lerpTime)
        {
            float timer = 0;
            float percentage = 0;

            while (percentage < 1)
            {
                percentage = (timer / _lerpTime);
                _img.color = Color.Lerp(_start, _end, _animCurve.Evaluate(percentage));
                timer += Time.deltaTime;
                yield return null;
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void LerpCanvasGroup(ref UnityEngine.CanvasGroup _canvasGroup, float _start, float _end, float _time)
        {
            myStaticMB.StartCoroutine(Internal_LerpCanvasGroup(_canvasGroup, _start, _end, _time));
        }

        private static IEnumerator Internal_LerpCanvasGroup(UnityEngine.CanvasGroup _canvasGroup, float _start, float _end, float _lerpTime)
        {
            float timer = 0;
            float percentage = 0;

            while (percentage < 1)
            {
                percentage = (timer / _lerpTime);
                _canvasGroup.alpha = Mathf.Lerp(_start, _end, percentage);
                timer += Time.deltaTime;
                yield return null;
            }
            _canvasGroup.blocksRaycasts = Static.PercentAsBool(_end);
            _canvasGroup.interactable = Static.PercentAsBool(_end);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void ToggleSpriteAlpha(ref SpriteRenderer _sprRend)
        {
            float goalAlpha = -(_sprRend.color.a - 1);
            Color goalColor = _sprRend.color;
            goalColor.a = goalAlpha;
            _sprRend.color = goalColor;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Contracts.Pure]
        public static void LerpSpriteAlpha(ref SpriteRenderer _sprRend, float _time, float _startAlpha, float _endAlpha)
        {
            myStaticMB.StartCoroutine(Internal_LerpSpriteRenderer(_sprRend, _startAlpha, _endAlpha, _time));
        }


        private static IEnumerator Internal_LerpSpriteRenderer(UnityEngine.SpriteRenderer _spriteRenderer, float _start, float _end, float _lerpTime)
        {
            float timer = 0;
            float percentage = 0;
            Color startCol = _spriteRenderer.color;
            Color endCol = _spriteRenderer.color;
            startCol.a = _start;
            startCol.a = _end;
            while (percentage < 1)
            {
                percentage = (timer / _lerpTime);
                _spriteRenderer.color = Color.Lerp(startCol, endCol, percentage);
                timer += Time.deltaTime;
                yield return null;
            }
        }
        #endregion

        #region String Formatting

        #region Money

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string FormatAsMoney(ulong _value)
        {
            return string.Format("{0:C0}", _value);
        }

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string FormatAsMoney(long _value)
        {
            return string.Format("{0:C0}", _value);
        }

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string FormatAsMoney(float _value)
        {
            return string.Format("{0:C0}", _value);
        }

        #endregion

        [System.Diagnostics.Contracts.Pure]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static string FormatAsNumber(float _value)
        {
            return _value.ToString("N0");
        }

        #endregion
    }
}