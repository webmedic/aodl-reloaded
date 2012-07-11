/*************************************************************************
 *
 * DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER
 * 
 * Copyright 2008 Sun Microsystems, Inc. All rights reserved.
 * 
 * Use is subject to license terms.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy
 * of the License at http://www.apache.org/licenses/LICENSE-2.0. You can also
 * obtain a copy of the License at http://odftoolkit.org/docs/license.txt
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * 
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 ************************************************************************/

using System;
using System.Xml;
using AODL.Document.Styles;
using AODL.Document.Styles.Properties;
using AODL.Document;
using AODL.Document.SpreadsheetDocuments;

namespace AODL.Document.Styles
{
	/// <summary>
	/// ColumnStyle represent a table column style.
	/// </summary>
	public class ColumnStyle : AbstractStyle
	{
		/// <summary>
		/// Gets or sets the column properties.
		/// </summary>
		/// <value>The column properties.</value>
		public ColumnProperties ColumnProperties
		{
			get
			{
				foreach(IProperty property in this.PropertyCollection)
					if (property is ColumnProperties)
						return (ColumnProperties)property;
				ColumnProperties columnProperties	= new ColumnProperties(this);
				this.PropertyCollection.Add((IProperty)columnProperties);
				return columnProperties;
			}
			set
			{
				if (this.PropertyCollection.Contains((IProperty)value))
					this.PropertyCollection.Remove((IProperty)value);
				this.PropertyCollection.Add(value);
			}
		}

		/// <summary>
		/// Gets or sets the name of the parent style.
		/// </summary>
		/// <value>The name of the parent style.</value>
		public string ParentStyleName
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:parent-style-name",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:parent-style-name",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("parent-style-name", value, "style");
				this._node.SelectSingleNode("@style:parent-style-name",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Gets or sets the family style.
		/// </summary>
		/// <value>The family style.</value>
		public string FamilyStyle
		{
			get 
			{ 
				XmlNode xn = this._node.SelectSingleNode("@style:family",
					this.Document.NamespaceManager);
				if (xn != null)
					return xn.InnerText;
				return null;
			}
			set
			{
				XmlNode xn = this._node.SelectSingleNode("@style:family",
					this.Document.NamespaceManager);
				if (xn == null)
					this.CreateAttribute("family", value, "style");
				this._node.SelectSingleNode("@style:family",
					this.Document.NamespaceManager).InnerText = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ColumnStyle"/> class.
		/// </summary>
		/// <param name="document">The spreadsheet document.</param>
		public ColumnStyle(IDocument document)
		{
			this.Document					= document;
			this.InitStandards();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ColumnStyle"/> class.
		/// </summary>
		/// <param name="document">The spreadsheet document.</param>
		/// <param name="styleName">Name of the style.</param>
		public ColumnStyle(IDocument document, string styleName)
		{
			this.Document					= document;
			this.InitStandards();
			this.StyleName					= styleName;
		}

		/// <summary>
		/// Inits the standards.
		/// </summary>
		private void InitStandards()
		{
			this.NewXmlNode();
			this.PropertyCollection				= new IPropertyCollection();
			this.PropertyCollection.Inserted	+= PropertyCollection_Inserted;
			this.PropertyCollection.Removed		+= PropertyCollection_Removed;
			this.FamilyStyle					= "table-column";
//			this.Document.Styles.Add(this);
		}

		/// <summary>
		/// Create a new Xml node.
		/// </summary>
		private void NewXmlNode()
		{
			this.Node		= this.Document.CreateNode("style", "style");
			if (this.Document is SpreadsheetDocument)
				this.ParentStyleName			= "Default";
		}

		/// <summary>
		/// Create a XmlAttribute for propertie XmlNode.
		/// </summary>
		/// <param name="name">The attribute name.</param>
		/// <param name="text">The attribute value.</param>
		/// <param name="prefix">The namespace prefix.</param>
		private void CreateAttribute(string name, string text, string prefix)
		{
			XmlAttribute xa = this.Document.CreateAttribute(name, prefix);
			xa.Value		= text;
			this.Node.Attributes.Append(xa);
		}

		/// <summary>
		/// Properties the collection_ inserted.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IProperty)value).Node);
		}

		/// <summary>
		/// Properties the collection_ removed.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		private void PropertyCollection_Removed(int index, object value)
		{
			this.Node.RemoveChild(((IProperty)value).Node);
		}
	}
}

/*
 * $Log: ColumnStyle.cs,v $
 * Revision 1.2  2008/04/29 15:39:53  mt
 * new copyright header
 *
 * Revision 1.1  2007/02/25 08:58:47  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.3  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.2  2006/01/29 18:52:51  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 */