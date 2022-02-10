using IOC.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IOC
{
	/// <summary>
	/// IoC Container
	/// </summary>
	public class Container : IContainer
	{
		public string Name { get; private set; }

		private readonly Dictionary<Type, LifecycleType> _lifecycleTypeMap = new Dictionary<Type, LifecycleType>();
		private readonly Dictionary<Type, Type> _transientMap = new Dictionary<Type, Type>();
		private readonly Dictionary<Type, object> _singletonMap = new Dictionary<Type, object>();

		public Container(string containerName)
		{
			Name = containerName;
			Trace.TraceInformation($"New instance of {Name} created");
		}

		/// <summary>
		/// Register the mapping for inversion of control
		/// </summary>
		/// <typeparam name="From">Interface</typeparam>
		/// <typeparam name="To">Insatnce</typeparam>
		/// <param name="lifecycleType"></param>
		public void Register<From, To>(LifecycleType lifecycleType = LifecycleType.Transient)
		{
			switch (lifecycleType)
			{
				case LifecycleType.Transient:
					AddTransient<From, To>();
					break;
				case LifecycleType.Singleton:
					AddSingleton<From, To>();
					break;
			}
		}

		/// <summary>
		/// Register the mapping for a transient instance
		/// </summary>
		/// <typeparam name="From">Interface</typeparam>
		/// <typeparam name="To">Insatnce</typeparam>
		/// <exception cref="Exception"></exception>
		public void AddTransient<From, To>()
		{
			try
			{
				_transientMap.Add(typeof(From), typeof(To));
				_lifecycleTypeMap.Add(typeof(From), LifecycleType.Transient);
			}
			catch (Exception ex)
			{
				throw new Exception($"Unable to register {typeof(From).Name} for {typeof(To).Name} as {LifecycleType.Transient}", ex);
			}
		}

		/// <summary>
		/// Register the mapping for as singleton instance
		/// </summary>
		/// <typeparam name="From">Interface</typeparam>
		/// <typeparam name="To">Insatnce</typeparam>
		/// <exception cref="Exception"></exception>
		public void AddSingleton<From, To>()
		{
			try
			{
				var instance = Create(typeof(To));

				if (instance != null)
				{
					_singletonMap.Add(typeof(From), instance);
					_lifecycleTypeMap.Add(typeof(From), LifecycleType.Singleton);
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Unable to register {typeof(From).Name} for {typeof(To).Name} as {LifecycleType.Singleton}", ex);
			}
		}

		/// <summary>
		/// Resolves the Instance for the specifed type
		/// </summary>
		/// <typeparam name="T">Interface</typeparam>
		/// <returns>The object instance for the specified interface</returns>
		public T Resolve<T>()
		{
			return (T)Resolve(typeof(T));
		}

		/// <summary>
		/// Creates an object instance of the specified type
		/// </summary>
		/// <param name="type">Interface</param>
		/// <returns>An object instance</returns>
		/// <exception cref="Exception"></exception>
		public object Create(Type type)
		{
			object instance = null;

			try
			{
				ConstructorInfo ctor = type.GetConstructors().First();
				ParameterInfo[] ctorParameters = ctor.GetParameters();

				if (ctorParameters.Length == 0)
				{
					instance = Activator.CreateInstance(type);
				}

				if (instance == null)
				{
					List<object> parameters = new List<object>();

					foreach (var p in ctorParameters)
					{
						parameters.Add(Resolve(p.ParameterType));
					}

					instance = ctor.Invoke(parameters.ToArray());
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Object instance for {type.Name} could not be created", ex);
			}

			return instance;
		}

		/// <summary>
		/// Resolves the instance for the given type
		/// </summary>
		/// <param name="type"></param>
		/// <returns>An object instance</returns>
		private object Resolve(Type type)
		{
			object resolvedObject = null;
			LifecycleType lifecycleType = GetLifecycleType(type);

			switch (lifecycleType)
			{
				case LifecycleType.Transient:
					resolvedObject = GetTransient(type);
					break;
				case LifecycleType.Singleton:
					resolvedObject = GetSingleton(type);
					break;
			}

			return resolvedObject;
		}

		/// <summary>
		/// Gets the lifecycle type for the specified type
		/// </summary>
		/// <param name="type">Interface</param>
		/// <returns>THe lifecyle type</returns>
		/// <exception cref="Exception"></exception>
		private LifecycleType GetLifecycleType(Type type)
		{
			LifecycleType lifecycleType;

			try
			{
				lifecycleType = _lifecycleTypeMap[type];
			}
			catch (Exception ex)
			{
				throw new Exception($"{type.Name} could not be resolved, or has not been added to container", ex);
			}

			return lifecycleType;
		}

		/// <summary>
		/// Resolves the instance for a transient object
		/// </summary>
		/// <param name="type">Interface</param>
		/// <returns>An object instance</returns>
		/// <exception cref="Exception"></exception>
		private object GetTransient(Type type)
		{
			Type resolvedType;

			try
			{
				resolvedType = _transientMap[type];
			}
			catch (Exception ex)
			{
				throw new Exception($"{LifecycleType.Transient} {type.Name} could not be resolved, or has not been added to container", ex);
			}

			return Create(resolvedType);
		}

		/// <summary>
		/// Resolves the instance for a singleton object
		/// </summary>
		/// <param name="type">Interface</param>
		/// <returns>An object instance</returns>
		/// <exception cref="Exception"></exception>
		private object GetSingleton(Type type)
		{
			object singleton;

			try
			{
				singleton = _singletonMap[type];
			}
			catch (Exception ex)
			{
				throw new Exception($"{LifecycleType.Singleton} {type.Name} could not be resolved, or has not been added to container", ex);
			}

			return singleton;
		}
	}
}
