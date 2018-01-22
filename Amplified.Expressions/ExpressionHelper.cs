using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Amplified.Expressions
{
    public static class ExpressionHelper
    {
        /// <summary>Resolves the value of the expression.</summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <typeparam name="TResult">The return type of the expression.</typeparam>
        /// <returns>The result of evaluating the expression.</returns>
        /// <remarks>
        /// <para>
        /// All expressions with return values are supported. If an expression cannot be evaluated, the implementation 
        /// will fall back to the expensive operation of compiling the expression. 
        /// </para>
        /// <para>
        /// Visual Basic Power expressions is an example of an expression that is evaluated using compilation, because 
        /// it is not natively representable in C#.
        /// </para>
        /// </remarks>
        public static TResult ResolveValue<TResult>(Expression<Func<TResult>> expression)
        {
            return (TResult) ValueResolver.ResolveValue(expression.Body);
        }
        
        /// <summary>Resolves the value of the expression.</summary>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns>The result of evaluating the expression.</returns>
        /// <remarks>
        /// <para>
        /// All expressions with return values are supported. If an expression cannot be evaluated, the implementation 
        /// will fall back to the expensive operation of compiling the expression. 
        /// </para>
        /// <para>
        /// Visual Basic Power expressions is an example of an expression that is evaluated using compilation, because 
        /// it is not natively representable in C#.
        /// </para>
        /// </remarks>
        public static object ResolveValue(this Expression expression)
        {
            return ValueResolver.ResolveValue(expression);
        }
        
        /// <summary>Resolves the values of the expressions.</summary>
        /// <param name="expressions">The expressions to evaluate.</param>
        /// <returns>The result of evaluating the expression.</returns>
        /// <remarks>
        /// <para>
        /// All expressions with return values are supported. If an expression cannot be evaluated, the implementation 
        /// will fall back to the expensive operation of compiling the expression. 
        /// </para>
        /// <para>
        /// Visual Basic Power expressions is an example of an expression that is evaluated using compilation, because 
        /// it is not natively representable in C#.
        /// </para>
        /// </remarks>
        public static object[] ResolveValues(IEnumerable<Expression> expressions)
        {
            return ValueResolver.ResolveValues(expressions);
        }
    }
}