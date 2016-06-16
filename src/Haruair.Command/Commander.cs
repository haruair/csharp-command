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

		public ICommandResolver CommandResolver {
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
			this.Add (typeof(T));
			return this;
		}

		public void Parse(string[] args) {

			if (RequestParser == null) {
				RequestParser = new BasicRequestParser ();
			}

			if (CommandResolver == null) {
				CommandResolver = new BasicCommandResolver ();
			}

			if (Prompter == null) {
				Prompter = new BasicConsolePrompter ();
			}

			var request = RequestParser.Parse (args);

			CommandResolver.Commands = this.Commands;

			var meta = CommandResolver.Match (request);

			if (meta != null) {
				var methodParameters = meta.MethodInfo.GetParameters ();
				var methodParamAttributes = (Parameter[]) Attribute.GetCustomAttributes (meta.MethodInfo, typeof(Parameter));

				IList<string> parameters = new List<string> ();

				if (methodParameters.Length > 0) {
					var i = 0;
					foreach (var methodParameter in methodParameters) {
						var requestParam = request.Params.ElementAtOrDefault (i);
						var methodParamAttribute = methodParamAttributes
							.Where (p => p.Attribute.Equals (methodParameter.Name))
							.FirstOrDefault ();
						
						if (requestParam != null || methodParamAttribute.Required == false) parameters.Add (requestParam);
						i++;
					}
				}
				if (parameters.Count == methodParamAttributes.Length) {
					if (parameters.Count == 0) {
						meta.MethodInfo.Invoke (Activator.CreateInstance (meta.CommandType), null);
					} else {
						meta.MethodInfo.Invoke (Activator.CreateInstance (meta.CommandType), parameters.ToArray ());
					}
				} else {
					this.PrintMessage (request);
				}

			} else {
				this.PrintMessage (request);
			}
		}

		protected void PrintMessage (IRequest request) {
			var list = CommandResolver.Find (request);
			var identity = list.FirstOrDefault ();
			if (identity.MethodInfo != null) {
				Prompter.WriteLine ("Example of {0}:", request.Command);
			} else {
				Prompter.WriteLine ("Example: ");
			}
			this.PrintCommands (list);
		}

		protected void PrintCommands(IList<CommandMeta> metaList) {
			foreach (var meta in metaList) {
				Prompter.Write ("  {0}", meta.Method);
				if(meta.Alias != null) Prompter.Write (", {0}", meta.Alias);
				if (meta.MethodInfo != null) {
					var methodParamAttributes = (Parameter[]) Attribute.GetCustomAttributes (meta.MethodInfo, typeof(Parameter));
					foreach (var param in methodParamAttributes) {
						Prompter.Write (param.Required ? " <{0}>" : " [{0}]", param.Attribute);
					}
				}
				if(meta.Description != null) Prompter.WriteLine ("\t{0}", meta.Description);
				else
					Prompter.WriteLine ();
			}
		}
	}
}

