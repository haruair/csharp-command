using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Haruair.Command.Interface;

namespace Haruair.Command
{
	public class BasicCommandResolver : ICommandResolver
	{
		public IList<Type> Commands {
			get;
			set;
		}

		public CommandMeta Match (IRequest request) {
			if (request.Command == null || request.Method == null) {
				return null;
			}
			
			var commandList = ConvertCommandsToCommandMeta (Commands);
			var command = commandList.FirstOrDefault(p => (p.Method.Equals(request.Command) || p.Alias.Equals(request.Command)));

			if (command == null) {
				return null;
			}

			var methodList = ConvertCommandToCommandMeta (command.CommandType);
			var method = methodList.FirstOrDefault(p =>
			   (p.Method != null && p.Method.Equals(request.Method))
			   || (p.Alias != null && p.Alias.Equals(request.Method)));
			if (method == null) {
				return null;
			}

			return method;
		}

		public IList<CommandMeta> Find (IRequest request) {
			var commandList = ConvertCommandsToCommandMeta (Commands);

			if (request.Command == null)
				return commandList;
			
			var command = commandList.FirstOrDefault(p => (p.Method.Equals(request.Command) || p.Alias.Equals(request.Command)));

			if (command == null && commandList.Count > 0) {
				return commandList;
			}

			var methodList = ConvertCommandToCommandMeta(command.CommandType);

			if (request.Method == null)
				return methodList;

			return methodList;
		}

		protected IList<CommandMeta> ConvertCommandsToCommandMeta (IList<Type> types) {
			var list = new List<CommandMeta> ();
			foreach (var type in types) {
				var attributes = Attribute.GetCustomAttributes (type);
				list.Add (ConvertToCommandMeta(attributes, type));
			}

			return list;
		}

		protected IList<CommandMeta> ConvertCommandToCommandMeta (Type type) {
			var list = new List<CommandMeta> ();
			foreach (var method in type.GetMethods ()) {
				var attributes = Attribute.GetCustomAttributes (method);
				list.Add (ConvertToCommandMeta(attributes, type, method));
			}
			return list;
		}

		protected CommandMeta ConvertToCommandMeta (Attribute[] attributes, Type type) {
			if (type == null)
				return null;

			var command = (Command)Attribute.GetCustomAttribute (type, typeof(Command));
			var usage = (Usage)Attribute.GetCustomAttribute (type, typeof(Usage));

			var meta = new CommandMeta
			{
				Alias = command?.Alias,
				Method = command?.Method,
				Description = usage?.Description,
				CommandType = type,
				MethodInfo = null
			};

			return meta;
		}

		protected CommandMeta ConvertToCommandMeta (Attribute[] attributes, Type type, MethodInfo method) {
			if (method == null)
				return null;
			
			var command = (Command)Attribute.GetCustomAttribute (method, typeof(Command));
			var usage = (Usage)Attribute.GetCustomAttribute (method, typeof(Usage));

			var meta = new CommandMeta
			{
				Alias = command?.Alias,
				Method = command?.Method,
				Description = usage?.Description,
				CommandType = type,
				MethodInfo = method
			};

			return meta;
		}
	}
}
