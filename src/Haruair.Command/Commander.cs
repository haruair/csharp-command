using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Haruair.Command.Interface;

namespace Haruair.Command
{
	public class Commander
	{
		protected IList<Type> Commands {
			get;
			set;
		}

		public IRequestParser RequestParser {
			get;
			set;
		}

		public IPrompter Prompter {
			get;
			set;
		}

		public Commander ()
		{
			this.Commands = new List<Type> ();
		}

		public Commander Add (Type type) {
			this.Commands.Add (type);
			return this;
		}

		public Commander Add<T> () {
			this.Commands.Add (typeof(T));
			return this;
		}

		public void Parse(string[] args) {

			if (RequestParser == null) {
				RequestParser = new BasicRequestParser ();
			}

			if (Prompter == null) {
				Prompter = new BasicConsolePrompter ();
			}

			var request = RequestParser.Parse (args);

			var metaList = this.ConvertCommands (this.Commands);

			if (request.Command == null) {
				Prompter.WriteLine ("Example: ");
				this.PrintCommands (metaList);
			} else if (request.Command != null && request.Method == null) {
				var meta = this.FindByCommand (request.Command, metaList);
				Prompter.WriteLine ("Example of {0}:", request.Command);
				if (meta.Command == null) {
					this.PrintCommands (metaList);
				} else {
					var methodList = this.ConvertMethods (meta.Command);
					this.PrintCommands (methodList != null && methodList.Count > 0 ? methodList : metaList);
				}
			} else {
				var meta = this.FindByCommand (request.Command, metaList);
				var methodList = this.ConvertMethods (meta.Command);
				var methodMeta = this.FindByCommand (request.Method, methodList);
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
				Prompter.Write ("  {0}", meta.Method);
				if(meta.Alias != null) Prompter.Write (", {0}", meta.Alias);
				if(meta.Description != null) Prompter.WriteLine ("\t{0}", meta.Description);
				else
					Prompter.WriteLine ();
			}
		}
	}
}

