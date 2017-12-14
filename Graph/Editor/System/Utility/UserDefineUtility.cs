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
    }

}