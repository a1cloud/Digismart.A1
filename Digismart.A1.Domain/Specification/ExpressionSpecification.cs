﻿
using System;
using System.Linq.Expressions;

namespace Digismart.A1.Domain.Specification
{
    /// <summary>
    /// Represents the specification which is represented by the corresponding
    /// LINQ expression.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    internal sealed class ExpressionSpecification<T> : Specification<T>
    {
        private Expression<Func<T, bool>> expression;

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="expression">The LINQ expression which represents the current
        /// specification.</param>
        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this.expression = expression;
        }

        /// <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public override Expression<Func<T, bool>> GetExpression()
        {
            return this.expression;
        }
    }
}