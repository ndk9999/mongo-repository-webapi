using MQS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MQS.Application.Common.Interfaces
{
	public interface IDocumentQuery<TDocument> where TDocument : IDocument
	{
		Expression<Func<TDocument, bool>> BuildFilterExpression(IApplicationDbContext context);
	}
}
