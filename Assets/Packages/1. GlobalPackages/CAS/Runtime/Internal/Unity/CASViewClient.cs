﻿//  Copyright © 2024 CAS.AI. All rights reserved.

#if UNITY_EDITOR
using System;
using System.Linq.Expressions;
using UnityEngine;

namespace CAS.Unity
{
    [Serializable]
    internal sealed class CASViewClient : CASViewBase
    {
        internal static bool emulateTabletScreen = false;

        private CASManagerBehaviour _behaviour;
        private bool _waitImpressionEvent = false;

        public bool active = false;
        public bool loaded = false;
        public AdError lastError = AdError.Internal;

        public override int refreshInterval { get; set; }

        public override bool isReady
        {
            get { return loaded && _manager.IsEnabledAd(AdType.Banner); }
        }

        internal CASViewClient(CASManagerClient client, CASManagerBehaviour behaviour, AdSize size) : base(client, size)
        {
            this._behaviour = behaviour;
            if (behaviour.IsAutoload(AdType.Banner))
                Load();
            refreshInterval = MobileAds.settings.bannerRefreshInterval;
        }

        public override void Dispose()
        {
            _behaviour.RemoveAdViewFromFactory(this);
        }

        public override void Load()
        {
            if (_manager.IsEnabledAd(AdType.Banner))
            {
                if (!loaded)
                    CallAdAction(AdActionCode.LOADED, 0.5f);
                return;
            }
            lastError = AdError.ManagerIsDisabled;
            CallAdAction(AdActionCode.FAILED);
        }

        public override void SetActive(bool active)
        {
            if (active)
            {
                if (!_manager.IsEnabledAd(AdType.Banner))
                {
                    lastError = AdError.ManagerIsDisabled;
                    CallAdAction(AdActionCode.FAILED);
                    return;
                }
            }
            this.active = active;
        }

        protected override void SetPositionPxNative(int position, int x, int y)
        {
            if (x == 0 && y == 0)
            {
                SetPositionNative(position, x, y);
            }
            else
            {
                float scale = MobileAds.GetDeviceScreenScale();
                _positionX = (int)(x / scale);
                _positionY = (int)(y / scale);
                SetPositionNative(position, _positionX, _positionY);
            }
        }

        protected override void SetPositionNative(int position, int x, int y)
        {
            CASFactory.UnityLog("Banner position changed to " + ((AdPosition)position).ToString() + " with offset: x=" + x + ", y=" + y);
        }

        public void OnGUIAd(GUIStyle style)
        {
            if (active && isReady)
            {
                if (_waitImpressionEvent)
                {
                    _waitImpressionEvent = false;
                    CallAdAction(AdActionCode.IMPRESSION);
                }
                CalculateAdRectOnScreen();
                var rect = new Rect(rectInPixels);
                var totalHeight = rect.height;
                rect.height = totalHeight * 0.65f;
                if (GUI.Button(rect, "CAS.AI " + size.ToString() + " Ad", style))
                    CallAdAction(AdActionCode.CLICKED);

                rect.y += rect.height;
                rect.height = totalHeight * 0.35f;

                emulateTabletScreen = GUI.Toggle(rect, emulateTabletScreen,
                    emulateTabletScreen ? "Switch to phone" : "Switch to tablet", style);
            }
        }

        private void CallAdAction(int action, float delay = 0.0f)
        {
            _behaviour.Post(() =>
            {
                if (action == AdActionCode.LOADED)
                {
                    CalculateAdRectOnScreen();
                    loaded = true;
                    _waitImpressionEvent = true;

                }
                HandleCallback(action, 0, (int)lastError, null);
            }, delay);
        }

        private void CalculateAdRectOnScreen()
        {
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            var safeArea = Screen.safeArea;
            float scale = MobileAds.GetDeviceScreenScale();
            bool isPortrait = screenWidth < screenHeight;

            AdSize targetSize;
            if (size == AdSize.SmartBanner)
                targetSize = emulateTabletScreen ? AdSize.Leaderboard : AdSize.Banner;
            else
                targetSize = size;

            Rect result = new Rect();
            switch (targetSize)
            {
                case AdSize.AdaptiveBanner:
                    result.width = Mathf.Min(screenWidth, 728.0f * scale);
                    result.height = (emulateTabletScreen ? 90.0f : 50.0f) * scale;
                    break;
                case AdSize.Leaderboard:
                    result.width = 728.0f * scale;
                    result.height = 90.0f * scale;
                    break;
                case AdSize.MediumRectangle:
                    result.width = 300.0f * scale;
                    result.height = 250.0f * scale;
                    break;
                case AdSize.AdaptiveFullWidth:
                    result.width = screenWidth;
                    result.height = (emulateTabletScreen ? 90.0f : 50.0f) * scale;
                    break;
                case AdSize.ThinBanner:
                    result.width = screenWidth;
                    if (emulateTabletScreen)
                        result.height = (isPortrait ? 90.0f : 50.0f) * scale;
                    else
                        result.height = (isPortrait ? 50.0f : 32.0f) * scale;
                    break;
                default:
                    result.width = 320.0f * scale;
                    result.height = 50.0f * scale;
                    break;
            }

            switch (_position)
            {
                case AdPosition.TopCenter:
                case AdPosition.TopLeft:
                case AdPosition.TopRight:
                    result.y = _positionY * scale;
                    break;
                case AdPosition.BottomCenter:
                case AdPosition.BottomLeft:
                case AdPosition.BottomRight:
                    result.y = screenHeight - result.height - (_positionY * scale);
                    break;
                default:
                    result.y = safeArea.height * 0.5f + safeArea.y - result.height * 0.5f;
                    break;
            }

            switch (_position)
            {
                case AdPosition.TopLeft:
                case AdPosition.BottomLeft:
                case AdPosition.MiddleLeft:
                    result.x = _positionX * scale;
                    break;
                case AdPosition.TopRight:
                case AdPosition.BottomRight:
                case AdPosition.MiddleRight:
                    result.x = screenWidth - result.width - (_positionX * scale);
                    break;
                default:
                    result.x = safeArea.width * 0.5f + safeArea.x - result.width * 0.5f;
                    break;
            }

            result.y = Mathf.Clamp(result.y,
                min: safeArea.y,
                max: screenHeight - result.height - (screenHeight - safeArea.yMax));

            result.x = Mathf.Clamp(result.x,
                min: safeArea.x,
                max: screenWidth - result.width - (screenWidth - safeArea.xMax));

            rectInPixels = result;
        }
    }

    [Serializable]
    internal class CASFullscreenView
    {
        public bool active = false;
        public bool loaded = false;

        public AdError lastError;
        public AdType type;

        private CASManagerBehaviour _behaviour;

        internal CASFullscreenView(CASManagerBehaviour behaviour, AdType type)
        {
            _behaviour = behaviour;
            this.type = type;
        }

        public void Load()
        {
            if (_behaviour.client.IsEnabledAd(type))
            {
                if (!loaded)
                    CallAdAction(AdActionCode.LOADED, 1.0f);
                return;
            }
            lastError = AdError.ManagerIsDisabled;
            CallAdAction(AdActionCode.FAILED);
        }

        public AdError? GetReadyError()
        {
            if (_behaviour.isFullscreenAdVisible)
                return AdError.AlreadyDisplayed;
            if (!_behaviour.client.IsEnabledAd(type))
                return AdError.ManagerIsDisabled;
            if (type == AdType.Interstitial
               && _behaviour._settings.lastInterImpressionTimestamp + _behaviour._settings.interstitialInterval > Time.time)
                return AdError.IntervalNotYetPassed;
            if (!loaded)
                return AdError.NotReady;
            return null;
        }

        public void Show()
        {
            var error = GetReadyError();
            if (error.HasValue)
            {
                lastError = error.Value;
                CallAdAction(AdActionCode.SHOW_FAILED);
                return;
            }
            active = true;
            loaded = false;
            lastError = AdError.NotReady;
            CallAdAction(AdActionCode.SHOWN);
            CallAdAction(AdActionCode.IMPRESSION);
        }

        public virtual void OnGUIAd(GUIStyle style)
        {
            if (!active)
                return;

            string header = "CAS.AI " + type.ToString() + " Ad. Nice job!\nClick to Close";
            if (type == AdType.Rewarded)
            {
                float width = Screen.width;
                float halfHeight = Screen.height * 0.5f;
                bool isClosed = GUI.Button(new Rect(0, 0, width, halfHeight), header, style);
                bool isCompleted = GUI.Button(new Rect(0, halfHeight, width, halfHeight),
                    "Click to earn Reward", style);
                if (isClosed || isCompleted)
                {
                    if (isCompleted)
                    {
                        CallAdAction(AdActionCode.CLICKED);
                        CallAdAction(AdActionCode.COMPLETED);
                    }
                    CallAdAction(AdActionCode.CLOSED);
                }
            }
            else if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), header, style))
            {
                if (type == AdType.Interstitial)
                    _behaviour._settings.lastInterImpressionTimestamp = Time.time;
                CallAdAction(AdActionCode.CLICKED);
                CallAdAction(AdActionCode.CLOSED);
            }
        }

        private void CallAdAction(int action, float delay = 0.0f)
        {
            _behaviour.Post(() =>
            {
                if (action == AdActionCode.LOADED)
                {
                    loaded = true;
                }
                else if (action == AdActionCode.CLOSED)
                {
                    active = false;
                    if (_behaviour.IsAutoload(type))
                        Load();
                }
                _behaviour.client.HandleCallback(action, (int)type, (int)lastError, null);
            }, delay);
        }
    }
}
#endif