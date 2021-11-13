using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Domain.Common
{
	public abstract class Document : IDocument
	{
		public Guid Id { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
