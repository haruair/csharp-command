using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Haruair.Command
{
	public class Commander
	{
		protected IList<Type> Commands {
			get;
			set;
		}

		public Commander ()
		{
			this.Commands = new List<Type> ();
		}

		public void Add (Type type) {
			this.Commands.Add (type);
		}

		public void Run(string[] args) {
			var metaList = this.ConvertCommands (this.Commands);

			if (args.Length == 0) {
				Console.WriteLine ("Example: ");
				this.PrintCommands (metaList);
			} else if (args.Length == 1) {
				var meta = this.FindByCommand (args [0], metaList);
				Console.WriteLine ("Example of {0}:", args [0]);
				var methodList = this.ConvertMethods (meta.Command);
				this.PrintCommands (methodList);
			} else {
				var meta = this.FindByCommand (args [0], metaList);
				var methodList = this.ConvertMethods (meta.Command);
				var methodMeta = this.FindByCommand (args [1], methodList);
				if (methodMeta == null) {
					this.PrintCommands (methodList);
				} else {
					methodMeta.CallMethod.Invoke (Activator.CreateInstance (methodMeta.Command), null);
				}
			}
		}

		protected CommandMeta FindByCommand(string key, IList<CommandMeta> metas) {
			return metas.Where (p => (p.Alias != null && p.Alias.Equals (key))
				|| (p.Method != null && p.Method.Equals (key))).FirstOrDefault ();
		}

		protected IList<CommandMeta> ConvertCommands(IList<Type> types) {
			var metaList = new List<CommandMeta>();
			foreach (var type in types) {
				var attrs = Attribute.GetCustomAttributes (type);
				metaList.Add (this.ConvertToCommandMeta (attrs, type));
			}
			return metaList;
		}

		protected IList<CommandMeta> ConvertMethods(Type type) {
			var metaList = new List<CommandMeta>();
			var methods = type.GetMethods ();
			foreach (var method in methods) {
				var attrs = Attribute.GetCustomAttributes (method);
				if (attrs.Length > 0) {
					metaList.Add (this.ConvertToCommandMeta (attrs, type, method));
				}
			}
			return metaList;
		}

		protected CommandMeta ConvertToCommandMeta (Attribute[] attrs, Type type = null, MethodInfo method = null) {

			Command command = (Command) attrs.Where (p => p is Command).FirstOrDefault();
			Usage usage = (Usage) attrs.Where (p => p is Usage).FirstOrDefault();

			var meta = new CommandMeta () {
				Alias = command?.Alias,
				Method = command?.Method,
				Description = usage?.Description,
				Command = type,
				CallMethod = method
			};

			return meta;
		}

		protected void PrintCommands(IList<CommandMeta> metaList) {
			foreach (var meta in metaList) {
				Console.Write ("  {0}", meta.Method);
				if(meta.Alias != null) Console.Write (", {0}", meta.Alias);
				if(meta.Description != null) Console.WriteLine ("\t{0}", meta.Description);
				else
					Console.WriteLine ();
			}
		}
	}
}

