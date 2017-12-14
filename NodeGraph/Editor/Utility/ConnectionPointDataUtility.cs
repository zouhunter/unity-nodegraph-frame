using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.Scripting;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.Assertions.Comparers;
using System.Collections;
using System.Collections.Generic;

namespace NodeGraph
{
    public static class ConnectionPointDataUtility
    {
        public static Rect UpdateRegion(NodeGUI node, bool IsInput,  float yOffset, int index, int max)
        {
            var parentRegion = node.Region;
            Rect buttonRect = new Rect();
            if (IsInput)
            {

                var initialY = yOffset + (Settings.GUI.NODE_BASE_HEIGHT - Settings.GUI.INPUT_POINT_HEIGHT) / 2f;
                var marginY = initialY + Settings.GUI.FILTER_OUTPUT_SPAN * (index);

                buttonRect = new Rect(
                    0,
                    marginY,
                    Settings.GUI.INPUT_POINT_WIDTH,
                    Settings.GUI.INPUT_POINT_HEIGHT);
            }
            else
            {

                var initialY = yOffset + (Settings.GUI.NODE_BASE_HEIGHT - Settings.GUI.OUTPUT_POINT_HEIGHT) / 2f;
                var marginY = initialY + Settings.GUI.FILTER_OUTPUT_SPAN * (index);

                buttonRect = new Rect(
                    parentRegion.width - Settings.GUI.OUTPUT_POINT_WIDTH + 1f,
                    marginY,
                    Settings.GUI.OUTPUT_POINT_WIDTH,
                    Settings.GUI.OUTPUT_POINT_HEIGHT);
            }
            return buttonRect;
        }
        public static Rect GetGlobalPointRegion(bool IsInput,Rect buttonRect, NodeGUI node)
        {
            if (IsInput)
            {
                return GetInputPointRect(buttonRect, node);
            }
            else
            {
                return GetOutputPointRect(buttonRect,node);
            }
        }
        private static Rect GetOutputPointRect(Rect buttonRect, NodeGUI node)
        {
            var baseRect = node.Region;
            return new Rect(
                baseRect.x + baseRect.width - (Settings.GUI.CONNECTION_POINT_MARK_SIZE) / 2f,
                baseRect.y + buttonRect.y + 1f,
                Settings.GUI.CONNECTION_POINT_MARK_SIZE,
                Settings.GUI.CONNECTION_POINT_MARK_SIZE
            );
        }

        private static Rect GetInputPointRect(Rect buttonRect, NodeGUI node)
        {
            var baseRect = node.Region;
            return new Rect(
                baseRect.x - 2f,
                baseRect.y + buttonRect.y + 3f,
                Settings.GUI.CONNECTION_POINT_MARK_SIZE + 3f,
                Settings.GUI.CONNECTION_POINT_MARK_SIZE + 3f
            );
        }
    }
}