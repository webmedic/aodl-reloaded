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
using System.Collections;
using System.IO;
using System.Xml;

using AODL.Document.Content.Fields;
using AODL.Document.Export;
using AODL.Document.Import.OpenDocument.NodeProcessors;
using AODL.Document.SpreadsheetDocuments;
using AODL.Document.Styles.MasterStyles;
using AODL.Document.TextDocuments;
using ICSharpCode.SharpZipLib.Zip;

namespace AODL.Document.Import.OpenDocument
{
	public class DirInfo
	{
		private string dir		= string.Empty;
		private string dirpics	= string.Empty;

		public DirInfo(string dir, string dirpics)
		{
			this.dir = dir;
			this.dirpics = dirpics;
		}
		
		public string Dir {
			get { return dir; }
		}
		
		public string Dirpics {
			get { return dirpics; }
		}
	}
	
	/// <summary>
	/// OpenDocumentImporter - Importer for OpenDocuments in different formats.
	/// </summary>
	public class OpenDocumentImporter : IImporter, IPublisherInfo
	{
		private Guid folderGuid			= Guid.NewGuid();
		private DirInfo m_dirInfo = null;
		
		public DirInfo DirInfo {
			get { return m_dirInfo; }
		}

		public IDocument Document {
			get { return _document; }
			set { _document = value; }
		}
		
		
		/// <summary>
		/// The document to fill with content.
		/// </summary>
		private IDocument _document;

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenDocumentImporter"/> class.
		/// </summary>
		public OpenDocumentImporter()
		{
			string dir = Path.Combine(Environment.CurrentDirectory, folderGuid.ToString());
			m_dirInfo = new DirInfo(dir, Path.Combine(dir, "PicturesRead"));
			
			this._importError					= new ArrayList();
			
			this._supportedExtensions			= new ArrayList();
            this._supportedExtensions.Add(new DocumentSupportInfo(".ott", DocumentTypes.TextDocument));
			this._supportedExtensions.Add(new DocumentSupportInfo(".odt", DocumentTypes.TextDocument));
			this._supportedExtensions.Add(new DocumentSupportInfo(".ods", DocumentTypes.SpreadsheetDocument));

			this._author						= "Lars Behrmann, lb@OpenDocument4all.com";
			this._infoUrl						= "http://AODL.OpenDocument4all.com";
			this._description					= "This the standard importer of the OpenDocument library AODL.";
		}

		#region IExporter Member

		private ArrayList _supportedExtensions;
		/// <summary>
		/// Gets the document support infos.
		/// </summary>
		/// <value>The document support infos.</value>
		public ArrayList DocumentSupportInfos
		{
			get { return this._supportedExtensions; }
		}

		/// <summary>
		/// Imports the specified filename.
		/// </summary>
		/// <param name="document">The TextDocument to fill.</param>
		/// <param name="filename">The filename.</param>
		/// <returns>The created TextDocument</returns>
		public void Import(IDocument document, string filename)
		{
			try
			{
				this._document		= document;
				this.UnpackFiles(filename);
				this.ReadContent();
			}
			catch(Exception ex)
			{
				throw new ImporterException(string.Format(
					"Failed to import document '{0}'", filename), ex);
			}
		}
		
		public void DeleteUnpackedFiles()
		{
			if (Directory.Exists(m_dirInfo.Dir) == true)
				Directory.Delete(m_dirInfo.Dir, true);
		}

		private ArrayList _importError;
		/// <summary>
		/// Gets the import errors as ArrayList of strings.
		/// </summary>
		/// <value>The import errors.</value>
		public System.Collections.ArrayList ImportError
		{
			get
			{
				return this._importError;
			}
		}

		/// <summary>
		/// If the import file format isn't any OpenDocument
		/// format you have to return true and AODL will
		/// create a new one.
		/// </summary>
		/// <value></value>
		public bool NeedNewOpenDocument
		{
			get { return false; }
		}

		#endregion

		#region IPublisherInfo Member

		private string _author;
		/// <summary>
		/// The name the Author
		/// </summary>
		/// <value></value>
		public string Author
		{
			get
			{
				return this._author;
			}
		}

		private string _infoUrl;
		/// <summary>
		/// Url to a info site
		/// </summary>
		/// <value></value>
		public string InfoUrl
		{
			get
			{
				return this._infoUrl;
			}
		}

		private string _description;
		/// <summary>
		/// Description about the exporter resp. importer
		/// </summary>
		/// <value></value>
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		#endregion

		#region unpacking files and images

		/// <summary>
		/// Unpacks the files.
		/// </summary>
		/// <param name="file">The file.</param>
		private void UnpackFiles(string file)
		{
			if (!Directory.Exists(m_dirInfo.Dir))
				Directory.CreateDirectory(m_dirInfo.Dir);

			ZipInputStream s = new ZipInputStream(File.OpenRead(file));
			
			ZipEntry theEntry;
			while ((theEntry = s.GetNextEntry()) != null)
			{
				string directoryName = Path.GetDirectoryName(theEntry.Name);
				string fileName      = Path.GetFileName(theEntry.Name);

				if (directoryName != String.Empty)
					Directory.CreateDirectory(Path.Combine(m_dirInfo.Dir, directoryName));
				
				if (fileName != String.Empty)
				{
					FileStream streamWriter = File.Create(Path.Combine(m_dirInfo.Dir, theEntry.Name));
					// TODO: Switch this to MemoryStream which is accessible through a package factory
					int size = 2048;
					byte[] data = new byte[2048];
					while (true)
					{
						size = s.Read(data, 0, data.Length);
						if (size > 0)
						{
							streamWriter.Write(data, 0, size);
						}
						else
						{
							break;
						}
					}
					
					streamWriter.Close();
				}
			}
			s.Close();
			
			this.MovePictures();
			this.ReadResources();
		}

		/// <summary>
		/// Moves the pictures folder
		/// To avoid gdi errors.
		/// </summary>
		private void MovePictures()
		{
//			if (Directory.Exists(dir+"Pictures"))
//			{
//				if (Directory.Exists(dirpics))
//					Directory.Delete(dirpics, true);
//				Directory.Move(dir+"Pictures", dirpics);
//			}
		}

		/// <summary>
		/// Reads the resources.
		/// </summary>
		private void ReadResources()
		{
			this._document.DocumentConfigurations2		= new DocumentConfiguration2();
			this.ReadDocumentConfigurations2();

			this._document.DocumentMetadata				= new DocumentMetadata(this._document);
			this._document.DocumentMetadata.LoadFromFile(Path.Combine(m_dirInfo.Dir,DocumentMetadata.FileName));

			if (this._document is TextDocument)
			{
				((TextDocument)this._document).DocumentSetting				= new  AODL.Document.TextDocuments.DocumentSetting();
				string file		= AODL.Document.TextDocuments.DocumentSetting.FileName;
				((TextDocument)this._document).DocumentSetting.LoadFromFile(Path.Combine(m_dirInfo.Dir, file));

				((TextDocument)this._document).DocumentManifest				= new AODL.Document.TextDocuments.DocumentManifest();
				string folder	= AODL.Document.TextDocuments.DocumentManifest.FolderName;
				file			= AODL.Document.TextDocuments.DocumentManifest.FileName;
				((TextDocument)this._document).DocumentManifest.LoadFromFile(Path.Combine(m_dirInfo.Dir, Path.Combine(folder,file)));

				((TextDocument)this._document).DocumentStyles				= new AODL.Document.TextDocuments.DocumentStyles();
				file			= AODL.Document.TextDocuments.DocumentStyles.FileName;
				((TextDocument)this._document).DocumentStyles.LoadFromFile(Path.Combine(m_dirInfo.Dir, file));
			}
			else if (this._document is SpreadsheetDocument)
			{
				((SpreadsheetDocument)this._document).DocumentSetting				= new  AODL.Document.SpreadsheetDocuments.DocumentSetting();
				string file		= AODL.Document.SpreadsheetDocuments.DocumentSetting.FileName;
				((SpreadsheetDocument)this._document).DocumentSetting.LoadFromFile(Path.Combine(m_dirInfo.Dir,file));

				((SpreadsheetDocument)this._document).DocumentManifest				= new AODL.Document.SpreadsheetDocuments.DocumentManifest();
				string folder	= AODL.Document.SpreadsheetDocuments.DocumentManifest.FolderName;
				file			= AODL.Document.SpreadsheetDocuments.DocumentManifest.FileName;
				((SpreadsheetDocument)this._document).DocumentManifest.LoadFromFile(Path.Combine(m_dirInfo.Dir, Path.Combine(folder,file)));

				((SpreadsheetDocument)this._document).DocumentStyles				= new AODL.Document.SpreadsheetDocuments.DocumentStyles();
				file			= AODL.Document.SpreadsheetDocuments.DocumentStyles.FileName;
				((SpreadsheetDocument)this._document).DocumentStyles.LoadFromFile(Path.Combine(m_dirInfo.Dir, file));
			}

			this._document.DocumentPictures				= this.ReadImageResources(Path.Combine(m_dirInfo.Dir, "Pictures"));

			this._document.DocumentThumbnails			= this.ReadImageResources(Path.Combine(m_dirInfo.Dir, "Thumbnails"));

			//There's no really need to read the fonts.

			this.InitMetaData();
		}

		/// <summary>
		/// Reads the document configurations2.
		/// </summary>
		private void ReadDocumentConfigurations2()
		{
			if (!Directory.Exists(Path.Combine(m_dirInfo.Dir, DocumentConfiguration2.FolderName)))
				return;
			DirectoryInfo di		= new DirectoryInfo(Path.Combine(m_dirInfo.Dir, DocumentConfiguration2.FolderName));
			foreach(FileInfo fi	in di.GetFiles())
			{
				this._document.DocumentConfigurations2.FileName	= fi.Name;
				string line			= null;
				StreamReader sr		= new StreamReader(fi.FullName);
				while((line	= sr.ReadLine())!=null)
					this._document.DocumentConfigurations2.Configurations2Content	+=line;
				sr.Close();
				break;
			}
		}

		/// <summary>
		/// Reads the image resources.
		/// </summary>
		/// <param name="folder">The folder.</param>
		private DocumentPictureCollection ReadImageResources(string folder)
		{
			DocumentPictureCollection dpc	= new DocumentPictureCollection();
			//If folder not exists, return (folder will only unpacked if not empty)
			if (!Directory.Exists(folder))
				return dpc;
			//Only image files should be in this folder, if not -> Exception
			DirectoryInfo di				= new DirectoryInfo(folder);
			foreach(FileInfo fi in di.GetFiles())
			{
				DocumentPicture dp			= new DocumentPicture(fi.FullName);
				dpc.Add(dp);
			}

			return dpc;
		}

		#endregion

		/// <summary>
		/// Reads the content.
		/// </summary>
		private void ReadContent()
		{
			/*
			 * NOTICE:
			 * Do not change this order!
			 */

			// 1. load content file
			this._document.XmlDoc			= new XmlDocument();
			this._document.XmlDoc.Load(Path.Combine(m_dirInfo.Dir, "content.xml"));

			// 2. Read local styles
			LocalStyleProcessor lsp			= new LocalStyleProcessor(this._document, false);
			lsp.ReadStyles();

			// 3. Import common styles and read common styles
			this.ImportCommonStyles();
			lsp								= new LocalStyleProcessor(this._document, true);
			lsp.ReadStyles();
			
			if (_document is TextDocument)
			{
				FormsProcessor fp= new FormsProcessor(this._document);
				fp.ReadFormNodes();
				
				TextDocument td = _document as TextDocument;
				td.VariableDeclarations.Clear();
				
				XmlNode nodeText		= td.XmlDoc.SelectSingleNode(
					TextDocumentHelper.OfficeTextPath, td.NamespaceManager);
				if (nodeText != null)
				{
					XmlNode nodeVarDecls = nodeText.SelectSingleNode("text:variable-decls", td.NamespaceManager);
					if (nodeVarDecls != null)
					{
						foreach (XmlNode vd in nodeVarDecls.CloneNode(true).SelectNodes("text:variable-decl", td.NamespaceManager))
						{
							td.VariableDeclarations.Add(new VariableDecl(td, vd));
						}
						nodeVarDecls.InnerXml = "";
					}
				}
			}
			
			// 4. Register warnig events
			MainContentProcessor mcp = new MainContentProcessor(this._document);
			mcp.Warning	+= mcp_OnWarning;

			// 5. Read the content
			mcp.ReadContentNodes();

			// 6.1 load master pages and styles for TextDocument
			if (this._document is TextDocument)
			{
				MasterPageFactory.RenameMasterStyles(
					((TextDocument)this._document).DocumentStyles.Styles,
					this._document.XmlDoc, this._document.NamespaceManager);
				// Read the moved and renamed styles
				lsp = new LocalStyleProcessor(this._document, false);
				lsp.ReReadKnownAutomaticStyles();
				new MasterPageFactory(m_dirInfo).FillFromXMLDocument(this._document as TextDocument);
			}
		}

		/// <summary>
		/// If the common styles are placed in the DocumentStyles,
		/// they will be imported into the content file.
		/// </summary>
		public void ImportCommonStyles()
		{
			string xPathToStyles				= "office:document-styles/office:styles";
			string xPathOfficeDocument			= "office:document-content";

			XmlNode nodeStyles				= null;
			XmlNode nodeOfficeDocument		= null;

			if (this._document is TextDocument)
				nodeStyles					= ((TextDocument)this._document).DocumentStyles.Styles.SelectSingleNode(
					xPathToStyles, this._document.NamespaceManager);
			else if (this._document is SpreadsheetDocument)
				nodeStyles					= ((SpreadsheetDocument)this._document).DocumentStyles.Styles.SelectSingleNode(
					xPathToStyles, this._document.NamespaceManager);

			nodeOfficeDocument				= this._document.XmlDoc.SelectSingleNode(
				xPathOfficeDocument, this._document.NamespaceManager);

			if (nodeOfficeDocument != null && nodeStyles != null)
			{
				nodeStyles					= this._document.XmlDoc.ImportNode(nodeStyles, true);
				nodeOfficeDocument.AppendChild(nodeStyles);
			}
		}

		/// <summary>
		/// Inits the meta data.
		/// </summary>
		private void InitMetaData()
		{
			this._document.DocumentMetadata.ImageCount		= 0;
			this._document.DocumentMetadata.ObjectCount		= 0;
			this._document.DocumentMetadata.ParagraphCount	= 0;
			this._document.DocumentMetadata.TableCount		= 0;
			this._document.DocumentMetadata.WordCount		= 0;
			this._document.DocumentMetadata.CharacterCount	= 0;
			this._document.DocumentMetadata.LastModified	= DateTime.Now.ToString("s");
		}

		/// <summary>
		/// MCP_s the on warning.
		/// </summary>
		/// <param name="warning">The warning.</param>
		private void mcp_OnWarning(AODL.Document.Exceptions.AODLWarning warning)
		{
			this._importError.Add(warning);
		}

		private void TextContentProcessor_OnWarning(AODL.Document.Exceptions.AODLWarning warning)
		{
			this._importError.Add(warning);
		}
	}
}

/*
 * $Log: OpenDocumentImporter.cs,v $
 * Revision 1.10  2008/04/29 15:39:52  mt
 * new copyright header
 *
 * Revision 1.9  2007/08/15 11:53:40  larsbehr
 * - Optimized Mono related stuff
 *
 * Revision 1.8  2007/07/15 09:30:46  yegorov
 * Issue number:
 * Submitted by:
 * Reviewed by:
 *
 * Revision 1.5  2007/06/20 17:37:19  yegorov
 * Issue number:
 * Submitted by:
 * Reviewed by:
 *
 * Revision 1.2  2007/04/08 16:51:37  larsbehr
 * - finished master pages and styles for text documents
 * - several bug fixes
 *
 * Revision 1.1  2007/02/25 08:58:45  larsbehr
 * initial checkin, import from Sourceforge.net to OpenOffice.org
 *
 * Revision 1.5  2006/05/02 17:37:16  larsbm
 * - Flag added graphics with guid
 * - Set guid based read and write directories
 *
 * Revision 1.4  2006/02/05 20:03:32  larsbm
 * - Fixed several bugs
 * - clean up some messy code
 *
 * Revision 1.3  2006/02/02 21:55:59  larsbm
 * - Added Clone object support for many AODL object types
 * - New Importer implementation PlainTextImporter and CsvImporter
 * - New tests
 *
 * Revision 1.2  2006/01/29 18:52:14  larsbm
 * - Added support for common styles (style templates in OpenOffice)
 * - Draw TextBox import and export
 * - DrawTextBox html export
 *
 * Revision 1.1  2006/01/29 11:28:23  larsbm
 * - Changes for the new version. 1.2. see next changelog for details
 *
 * Revision 1.4  2005/12/18 18:29:46  larsbm
 * - AODC Gui redesign
 * - AODC HTML exporter refecatored
 * - Full Meta Data Support
 * - Increase textprocessing performance
 *
 * Revision 1.3  2005/12/12 19:39:17  larsbm
 * - Added Paragraph Header
 * - Added Table Row Header
 * - Fixed some bugs
 * - better whitespace handling
 * - Implmemenation of HTML Exporter
 *
 * Revision 1.2  2005/11/20 17:31:20  larsbm
 * - added suport for XLinks, TabStopStyles
 * - First experimental of loading dcuments
 * - load and save via importer and exporter interfaces
 *
 * Revision 1.1  2005/11/06 14:55:25  larsbm
 * - Interfaces for Import and Export
 * - First implementation of IExport OpenDocumentTextExporter
 *
 */