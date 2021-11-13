using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Utils.Attributes
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class MongoCollectionAttribute : Attribute
	{
		public string CollectionName { get; }

		public MongoCollectionAttribute(string collectionName)
		{
			CollectionName = collectionName;
		}
	}
}
