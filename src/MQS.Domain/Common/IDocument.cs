using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Domain.Common
{
	public interface IDocument
	{
		Guid Id { get; set; }

		DateTime CreatedAt { get; set; }
	}
}
