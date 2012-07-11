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

namespace BillGenerator.Model
{
	/// <summary>
	/// Summary for BillingItem.
	/// </summary>
	public class BillingItem
	{
		private string _item;
		/// <summary>
		/// Gets or sets the item.
		/// </summary>
		/// <value>The item.</value>
		public string Item
		{
			get { return this._item; }
			set { this._item = value; }
		}

		private string _itemNo;
		/// <summary>
		/// Gets or sets the item no.
		/// </summary>
		/// <value>The item no.</value>
		public string ItemNo
		{
			get { return this._itemNo; }
			set { this._itemNo = value; }
		}

		private double _price;
		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value>The price.</value>
		public double Price
		{
			get { return this._price; }
			set { this._price = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BillingItem"/> class.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="itemNo">The item no.</param>
		/// <param name="price">The price.</param>
		public BillingItem(string item, string itemNo, double price)
		{
			this._item = item;
			this._itemNo = itemNo;
			this._price = price;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BillingItem"/> class.
		/// </summary>
		public BillingItem() {}
	}
}
