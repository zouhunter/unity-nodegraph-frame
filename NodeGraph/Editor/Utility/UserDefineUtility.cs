using System.Linq;
using System.Collections.Generic;
using System;
namespace NodeGraph
{

    public class UserDefineUtility
    {
        private static List<Type> _controllerTypes;
        public static List<Type> CustomControllerTypes
        {
            get
            {
                if (_controllerTypes == null)
                {
                    _controllerTypes = BuildControllerTypeList();
                }
                return _controllerTypes;
            }
        }
        private static List<Type> BuildControllerTypeList()
        {
            var list = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var nodes = assembly.GetTypes()
                    .Where(t => t != typeof(NodeGraphController))
                    .Where(t => typeof(NodeGraphController).IsAssignableFrom(t));
                list.AddRange(nodes);
            }
            return list;
        }

        internal static NodeGraphController CreateController(NodeGraph.DataModel.ConfigGraph graph)
        {
            var type = CustomControllerTypes.Find(x => x.Name == graph.ControllerType);
            if(type != null)
            {
                var ctrl = System.Activator.CreateInstance(type,new object[] { graph });
                return (ctrl as NodeGraphController);
            }
            else
            {
                return null;
            }
        }
        private static Dictionary<Type,Type> customDrawer;
        internal static NodeDrawer GetCustomDrawer(Node @object)
        {
            InitDrawerTypes();
            if(customDrawer.ContainsKey(@object.GetType()))
            {
                var drawer = Activator.CreateInstance(customDrawer[@object.GetType()]);
                var obj = drawer as NodeDrawer;
                obj.target = @object;
                return obj;
            }
            return null;
        }
        internal static ConnectionDrawer GetCustomConnectionDrawer(Connection @object)
        {
            InitDrawerTypes();
            if (customDrawer.ContainsKey(@object.GetType()))
            {
                var drawer = Activator.CreateInstance(customDrawer[@object.GetType()]);
                var obj = drawer as ConnectionDrawer;
                obj.target = @object;
                return obj;
            }
            return null;

        }
        private static void InitDrawerTypes()
        {
            if (customDrawer == null)
            {
                customDrawer = new Dictionary<Type, Type>();
                var allDrawer = new List<Type>();
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var nodes = assembly.GetTypes()
                        .Where(t => t != typeof(NodeDrawer) && t!= typeof(ConnectionDrawer))
                        .Where(t => typeof(NodeDrawer).IsAssignableFrom(t) || typeof(ConnectionDrawer).IsAssignableFrom(t));
                    allDrawer.AddRange(nodes);
                }
                foreach (var type in allDrawer)
                {
                    CustomNodeGraphDrawer attr = type.GetCustomAttributes(typeof(CustomNodeGraphDrawer), false).FirstOrDefault() as CustomNodeGraphDrawer;

                    if (attr != null)
                    {
                        customDrawer.Add(attr.targetType, type);
                    }
                }
            }
        }
     
    }

}