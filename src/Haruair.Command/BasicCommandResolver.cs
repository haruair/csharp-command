﻿using System;
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
			
			var commandList = this.ConvertCommandsToCommandMeta (this.Commands);
			var command = commandList.Where (p => (p.Method.Equals(request.Command) || p.Alias.Equals(request.Command))).FirstOrDefault ();

			if (command == null) {
				return null;
			}

			var methodList = this.ConvertCommandToCommandMeta (command.Command);
			var method = methodList.Where (p => (p.Method.Equals(request.Method) || p.Alias.Equals(request.Method))).FirstOrDefault ();

			if (method == null) {
				return null;
			}

			return method;
		}

		public IList<CommandMeta> Find (IRequest request) {
			var commandList = this.ConvertCommandsToCommandMeta (this.Commands);

			if (request.Command == null)
				return commandList;
			
			var command = commandList.Where (p => (p.Method.Equals (request.Command) || p.Alias.Equals (request.Command))).FirstOrDefault ();
			var methodList = this.ConvertCommandToCommandMeta (command.Command);

			if (request.Method == null)
				return methodList;

			var method = methodList.Where (p => (p.Method.Equals (request.Method) || p.Alias.Equals (request.Method))).FirstOrDefault ();

			if (method == null && methodList.Count > 0) {
				return methodList;
			}

			if (command == null && commandList.Count > 0) {
				return commandList;
			}

			return null;
		}

		protected IList<CommandMeta> ConvertCommandsToCommandMeta (IList<Type> types) {
			var list = new List<CommandMeta> ();
			foreach (var type in types) {
				var attributes = Attribute.GetCustomAttributes (type);
				list.Add (this.ConvertToCommandMeta (attributes, type));
			}

			return list;
		}

		protected IList<CommandMeta> ConvertCommandToCommandMeta (Type type) {
			var list = new List<CommandMeta> ();
			foreach (var method in type.GetMethods ()) {
				var attributes = Attribute.GetCustomAttributes (method);
				list.Add (this.ConvertToCommandMeta(attributes, type, method));
			}
			return list;
		}

		protected CommandMeta ConvertToCommandMeta (Attribute[] attributes, Type type) {
			if (type == null)
				return null;

			var command = (Command)Attribute.GetCustomAttribute (type, typeof(Command));
			var usage = (Usage)Attribute.GetCustomAttribute (type, typeof(Usage));

			var meta = new CommandMeta () {
				Alias = command?.Alias,
				Method = command?.Method,
				Description = usage?.Description,
				Command = type,
				CallMethod = null
			};

			return meta;
		}

		protected CommandMeta ConvertToCommandMeta (Attribute[] attributes, Type type, MethodInfo method) {
			if (method == null)
				return null;
			
			var command = (Command)Attribute.GetCustomAttribute (method, typeof(Command));
			var usage = (Usage)Attribute.GetCustomAttribute (method, typeof(Usage));

			var meta = new CommandMeta () {
				Alias = command?.Alias,
				Method = command?.Method,
				Description = usage?.Description,
				Command = type,
				CallMethod = method
			};

			return meta;
		}
	}
}