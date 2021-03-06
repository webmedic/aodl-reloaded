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
using System.IO;
using AODL.Document;
using AODL.Document.Forms;
using System.Xml;
using AODL.Document.Content;

namespace AODL.Document.Forms.Controls
{
	public class ODFImage: ODFFormControl
	{
		public override string Type
		{
			get
			{
				return "image";
			}
		}

		/// <summary>
		/// Contains additional information about a control.
		/// </summary>
		public string Title
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:title", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:title", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("title", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Specifies the default value of the control
		/// </summary>
		public string Value
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:value", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:value", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("value", "form"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Links the control to an external file containing image data
		/// </summary>
		public string ImageData
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:image-data", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				string path = xn.InnerText;
				if (path[0] == '/')
					path = path.Substring(1);
                path.Replace('/', Path.DirectorySeparatorChar);
				return path;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:image-data", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("image-data", "form"));
				string path = value;
				if (value[0] != '/')
				{
					path = "/" + value;
				}
                path = path.Replace(Path.DirectorySeparatorChar, '/');
				nd.InnerText = path;
			}
		}

		/// <summary>
		/// Specifies whether or not a control can accept user input
		/// </summary>
		public XmlBoolean Disabled
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:disabled", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:disabled", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("disabled", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies whether or not a control is printed when a user prints 
		/// the document in which the control is contained
		/// </summary>
		public XmlBoolean Printable
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:printable", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:printable", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("printable", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies whether or not a control is included in the tabbing 
		/// navigation order
		/// </summary>
		public XmlBoolean TabStop
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:tab-stop", 
					this._document.NamespaceManager);
				if (xn == null) return XmlBoolean.NotSet;

				string s = xn.InnerText;	
				XmlBoolean at;
				switch (s)
				{
					case "true": at = XmlBoolean.True; break;
					case "false": at = XmlBoolean.False; break;
					default: at = XmlBoolean.NotSet; break;
				}
				return at;
			}
			set
			{
				string s;
				switch (value)
				{
					case XmlBoolean.True: s = "true"; break;
					case XmlBoolean.False: s = "false"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:tab-stop", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("tab-stop", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies the tabbing navigation order of a control within a form
		/// </summary>
		public int TabIndex
		{
			get
			{
				return int.Parse(this._node.SelectSingleNode("@form:tab-index", 
					this._document.NamespaceManager).InnerText);
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@form:tab-index", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("tab-index", "form"));
				nd.InnerText = value.ToString();
			}
		}

		/// <summary>
		/// Specifies the type of the button
		/// </summary>
		public ButtonType ButtonType
		{	
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@form:button-type", 
					this._document.NamespaceManager);
				if (xn == null) return ButtonType.NotSet;
				string s = xn.InnerText;	
				ButtonType bt;
				switch (s)
				{
					case "push": bt = ButtonType.Push; break;
					case "submit": bt = ButtonType.Submit; break;
					case "reset": bt = ButtonType.Reset; break;
					case "url": bt = ButtonType.Url; break;
					
					default: bt = ButtonType.NotSet; break;
				}
				return bt;
			}
			set
			{
				string s;
				switch (value)
				{
					case ButtonType.Push: s = "push"; break;
					case ButtonType.Submit: s = "submit"; break;
					case ButtonType.Reset: s = "reset"; break;
					case ButtonType.Url: s = "url"; break;
					default: return;
				}
				XmlNode nd = this._node.SelectSingleNode("@form:button-type", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this._node.Attributes.Append(this._document.CreateAttribute("button-type", "form"));
				nd.InnerText = s;
			}
		}

		/// <summary>
		/// Specifies the target frame
		/// </summary>
		public TargetFrame TargetFrame
		{	
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@office:target-frame", 
					this._document.NamespaceManager);
				if (xn == null) return TargetFrame.NotSet;
				
				string s = xn.InnerText;	
				TargetFrame tf;
				switch (s)
				{
					case "_self": tf = TargetFrame.Self; break;
					case "_blank": tf = TargetFrame.Blank; break;
					case "_parent": tf = TargetFrame.Parent; break;
					case "_top": tf = TargetFrame.Top; break;
					default: tf = TargetFrame.Blank; break;
				}
				return tf;
			}
			set
			{
				string s;
				switch (value)
				{
					case TargetFrame.Self: s = "_self"; break;
					case TargetFrame.Blank: s = "_blank"; break;
					case TargetFrame.Parent: s = "_parent";  break;
					case TargetFrame.Top: s = "_top"; break;
					default: s = "_blank"; break;
				}
				XmlNode nd = this._node.SelectSingleNode("@office:target-frame", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("target-frame", "office"));
				nd.InnerText = s;
			}
		}
		
		/// <summary>
		/// Specifies the URL that is loaded if a button is clicked
		/// </summary>
		public string TargetLocation
		{
			get
			{
				XmlNode xn = this._node.SelectSingleNode("@xlink:href", 
					this._document.NamespaceManager);
				if (xn == null) return null;
				return xn.InnerText;
			}
			set
			{
				XmlNode nd = this._node.SelectSingleNode("@xlink:href", 
					this._document.NamespaceManager);
				if (nd == null)
					nd = this.Node.Attributes.Append(this._document.CreateAttribute("href", "xlink"));
				nd.InnerText = value;
			}
		}

		/// <summary>
		/// Creates an ODFImage
		/// </summary>
		/// <param name="ParentForm">Parent form that the control belongs to</param>
		/// <param name="contentCollection">Collection of content where the control will be referenced</param>
		/// <param name="id">Control ID. Please specify a unique ID!</param>
		public ODFImage(ODFForm ParentForm, ContentCollection contentCollection, string id): base (ParentForm, contentCollection, id)
		{}

		/// <summary>
		/// Creates an ODFImage
		/// </summary>
		/// <param name="ParentForm">Parent form that the control belongs to</param>
		/// <param name="contentCollection">Collection of content where the control will be referenced</param>
		/// <param name="id">Control ID. Please specify a unique ID!</param>
		/// <param name="x">X coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="y">Y coordinate of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="width">Width of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		/// <param name="height">Height of the control in ODF format (eg. "1cm", "15mm", 3.2cm" etc)</param>
		public ODFImage(ODFForm ParentForm, ContentCollection contentCollection, string id, string x, string y, string width, string height): base (ParentForm, contentCollection, id, x, y, width, height)
		{}

		public ODFImage(ODFForm ParentForm, XmlNode node): base(ParentForm, node)
		{}

		protected override void CreateBasicNode()
		{
			XmlNode xe = this._document.CreateNode("image", "form");
			Node.AppendChild(xe);
			Node = xe;
			this.ControlImplementation = "ooo:com.sun.star.form.component.ImageButton";
		}
	}
}
