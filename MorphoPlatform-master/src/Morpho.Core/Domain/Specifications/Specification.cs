using System;
using System.Linq.Expressions;

namespace Morpho.Domain.Specifications
{
    /// <summary>
    /// Base specification interface for implementing business rules.
    /// </summary>
    /// <typeparam name="T">The type of entity the specification applies to</typeparam>
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        bool IsSatisfiedBy(T entity);
    }

    /// <summary>
    /// Base class for implementing specifications with common functionality.
    /// </summary>
    /// <typeparam name="T">The type of entity the specification applies to</typeparam>
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }

        public virtual bool IsSatisfiedBy(T entity)
        {
            return Criteria.Compile()(entity);
        }

        public Specification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification.Criteria;
        }
    }

    /// <summary>
    /// Specification that combines two specifications with AND logic.
    /// </summary>
    /// <typeparam name="T">The type of entity the specification applies to</typeparam>
    internal class AndSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> Criteria
        {
            get
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var leftVisitor = new ParameterReplacer(parameter);
                var rightVisitor = new ParameterReplacer(parameter);

                var left = leftVisitor.Visit(_left.Criteria.Body);
                var right = rightVisitor.Visit(_right.Criteria.Body);

                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
            }
        }
    }

    /// <summary>
    /// Specification that combines two specifications with OR logic.
    /// </summary>
    /// <typeparam name="T">The type of entity the specification applies to</typeparam>
    internal class OrSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> Criteria
        {
            get
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var leftVisitor = new ParameterReplacer(parameter);
                var rightVisitor = new ParameterReplacer(parameter);

                var left = leftVisitor.Visit(_left.Criteria.Body);
                var right = rightVisitor.Visit(_right.Criteria.Body);

                return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
            }
        }
    }

    /// <summary>
    /// Specification that negates another specification.
    /// </summary>
    /// <typeparam name="T">The type of entity the specification applies to</typeparam>
    internal class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _specification;

        public NotSpecification(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> Criteria
        {
            get
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var visitor = new ParameterReplacer(parameter);
                var body = visitor.Visit(_specification.Criteria.Body);

                return Expression.Lambda<Func<T, bool>>(Expression.Not(body), parameter);
            }
        }
    }

    /// <summary>
    /// Helper class for replacing parameters in expressions.
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        public ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(_parameter);
        }
    }
}