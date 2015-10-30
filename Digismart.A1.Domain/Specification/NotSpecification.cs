
using System;
using System.Linq.Expressions;

namespace Digismart.A1.Domain.Specification
{
    /// <summary>
    /// Represents the specification which indicates the semantics opposite to the given specification.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public class NotSpecification<T> : Specification<T>
    {
        private ISpecification<T> spec;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="specification">The specification to be reversed.</param>
        public NotSpecification(ISpecification<T> specification)
        {
            this.spec = specification;
        }

        /// <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            var body = Expression.Not(this.spec.GetExpression().Body);
            return Expression.Lambda<Func<T, bool>>(body, this.spec.GetExpression().Parameters);
        }
    }
}
