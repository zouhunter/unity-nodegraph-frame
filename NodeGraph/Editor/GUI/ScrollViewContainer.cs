using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace NodeGraph
{
    public class ScrollViewContainer
    {
        private Rect currentPosition;
        private ScrollView scrollView;
        private IMGUIContainer content;
        private VisualContainer scrollViewContent;
        private float zoomSize = 1;
        public readonly float minZoomSize = 0.3f;
        public readonly float maxZoomSize = 1.3f;
        private ZoomManipulator zoomMa;
        public Action onGUI { get; set; }
        public float ZoomSize
        {
            get
            {
                return zoomSize;
            }
            set
            {
                zoomSize = zoomMa.SetZoom(value);
            }
        }
        public Vector2 scrollPos
        {
            get
            {
                return scrollView.scrollOffset;
            }
        }

        public void Start(VisualElement root, Rect position)
        {
            this.currentPosition = position;
            CreateScrollViewContent(position);
            CreateScrollViewContainer(position);
            CreateScrollView(position);
            CreateZoomManipulator(position);
            root.Add(scrollView);
        }

        public void UpdateScale(Rect position)
        {
            currentPosition = position;

            scrollView.style.marginTop = position.y;
            scrollView.style.marginLeft = position.x;
            scrollView.style.width = position.width;
            scrollView.style.height = position.height;

            content.style.width = position.width / minZoomSize;//内部固定大小（但scale在作用下会实现与ScrollViewContent一样大）
            content.style.height = position.height / minZoomSize;

            scrollViewContent.style.width = position.width * zoomSize / minZoomSize;//缩放容器以动态改变ScrollView的内部尺寸
            scrollViewContent.style.height = position.height * zoomSize / minZoomSize;
        }

        private void CreateScrollView(Rect position)
        {
            scrollView = new ScrollView()
            {
                style =
                {
                     marginTop = position.y,
                     marginLeft = position.x,
                     width = position.width,
                     height = position.height,
                     backgroundColor = Color.clear,
                 },
                showHorizontal = true,
                showVertical = true,
            };
            scrollView.clippingOptions = VisualElement.ClippingOptions.ClipContents;
            scrollView.Add(scrollViewContent);
        }
        private void CreateScrollViewContainer(Rect position)
        {
            scrollViewContent = new VisualContainer()
            {
                style =
                {
                    width = position.width * zoomSize / minZoomSize,
                    height = position.height * zoomSize / minZoomSize,
                    backgroundColor = Color.clear,
                }
            };
            scrollViewContent.Add(content);
        }
        private void CreateScrollViewContent(Rect position)
        {
            content = new IMGUIContainer(OnGUI)
            {
                style =
                {
                  width =  position.width / minZoomSize,
                  height = position.height / minZoomSize,
                  backgroundColor = Color.clear,
                }
            };
        }
        private void CreateZoomManipulator(Rect position)
        {
            zoomMa = new ZoomManipulator(minZoomSize, maxZoomSize, content);
            zoomMa.onZoomChanged = OnZoomValueChanged;
            zoomMa.scrollPosGet = () => { return scrollView.scrollOffset; };
            scrollView.AddManipulator(zoomMa);
        }
        private void OnGUI()
        {
            if (onGUI != null)
            {
                onGUI.Invoke();
            }
            else
            {
                Debug.Log("empty ongui!");
            }
        }
        private void OnZoomValueChanged(Vector2 arg1, float arg2)
        {
            zoomSize = arg2;
            scrollView.scrollOffset = -arg1;
            scrollViewContent.style.width = currentPosition.width * zoomSize / minZoomSize;//缩放容器以动态改变ScrollView的内部尺寸
            scrollViewContent.style.height = currentPosition.height * zoomSize / minZoomSize;
        }

        public void Refesh()
        {
            content.MarkDirtyRepaint();
        }
    }

    public class ZoomManipulator : MouseManipulator, IManipulator
    {
        private VisualElement targetElement;
        private float minSize = 0.5f;
        private float maxSize = 2f;
        public readonly float zoomStep = 0.05f;

        public System.Action<Vector2, float> onZoomChanged { get; set; }
        public System.Func<Vector2> scrollPosGet { get; set; }

        public ZoomManipulator(float minSize, float maxSize, VisualElement element)
        {
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.targetElement = element;
            base.activators.Add(new ManipulatorActivationFilter
            {
                button = MouseButton.RightMouse,
                modifiers = EventModifiers.Alt
            });
        }

        public float SetZoom(float zoom)
        {
            var scale = Mathf.Clamp(zoom, minSize, maxSize);
            targetElement.transform.scale = Vector3.one * scale;
            var offset = -scrollPosGet.Invoke();
            if (onZoomChanged != null)
            {
                onZoomChanged.Invoke(offset, scale);
            }
            return scale;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            base.target.RegisterCallback<WheelEvent>(OnScroll, Capture.NoCapture);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            base.target.UnregisterCallback<WheelEvent>(OnScroll, Capture.NoCapture);
        }

        private void OnScroll(WheelEvent e)
        {
            Vector2 zoomCenter = VisualElementExtensions.ChangeCoordinatesTo(base.target, this.targetElement, e.localMousePosition);
            float zoomScale = 1f - e.delta.y * zoomStep;
            this.Zoom(zoomCenter, zoomScale);
            e.StopPropagation();
        }

        private void Zoom(Vector2 zoomCenter, float zoomScale)
        {
            var offset = -scrollPosGet.Invoke();
            Vector3 scale = this.targetElement.transform.scale;
            Vector2 min = this.targetElement.layout.min;
            float x = zoomCenter.x + min.x;
            float y = zoomCenter.y + min.y;
            offset += Vector2.Scale(new Vector2(x, y), scale);
            scale = Vector3.Scale(scale, new Vector3(zoomScale, zoomScale, 1f));
            scale.x = Mathf.Clamp(scale.x, minSize, maxSize);
            scale.y = Mathf.Clamp(scale.y, minSize, maxSize);
            offset -= Vector2.Scale(new Vector2(x, y), scale);

            this.targetElement.transform.scale = scale;

            if (onZoomChanged != null)
            {
                onZoomChanged.Invoke(offset, scale.x);
            }
        }
    }
}