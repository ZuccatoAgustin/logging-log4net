#region Copyright & License
//
// Copyright 2001-2005 The Apache Software Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Collections;

namespace log4net.Util
{
	/// <summary>
	/// Implementation of Lists collection for the <see cref="log4net.ThreadContext"/>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Implementation of Lists collection for the <see cref="log4net.ThreadContext"/>
	/// </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	public sealed class ThreadContextLists
	{
		private readonly ContextPropertiesBase m_properties;

		#region Public Instance Constructors

		/// <summary>
		/// Internal constructor
		/// </summary>
		/// <remarks>
		/// <para>
		/// Initializes a new instance of the <see cref="ThreadContextLists" /> class.
		/// </para>
		/// </remarks>
		internal ThreadContextLists(ContextPropertiesBase properties)
		{
			m_properties = properties;
		}

		#endregion Public Instance Constructors

		#region Public Instance Properties

		/// <summary>
		/// Gets the named list
		/// </summary>
		/// <value>
		/// The named list
		/// </value>
		/// <remarks>
		/// <para>
		/// Gets the named list
		/// </para>
		/// </remarks>
		public ThreadContextList this[string key]
		{
			get 
			{
				ThreadContextList list = null;

				object propertyValue = m_properties[key];
				if (propertyValue == null)
				{
					// List does not exist, create
					list = new ThreadContextList();
					m_properties[key] = list;
				}
				else
				{
					// Look for existing list
					list = propertyValue as ThreadContextList;
					if (list == null)
					{
						// Property is not set to a list!
						string propertyValueString = "(null)";

						try
						{
							propertyValueString = propertyValue.ToString();
						}
						catch
						{
						}

						LogLog.Error("ThreadContextLists: Request for list named ["+key+"] failed because a property with the same name exists which is a ["+propertyValue.GetType().Name+"] with value ["+propertyValueString+"]");

						list = new ThreadContextList();
					}
				}

				return list;
			}
		}

		#endregion Public Instance Properties
	}
}

