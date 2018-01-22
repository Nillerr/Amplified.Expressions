using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Amplified.Expressions
{
    internal static class ValueResolver
    {
        private const string ExpressionNotSupported = "The specified expression is not supported by this method.";

        internal static object[] ResolveValues(IEnumerable<Expression> expressions)
        {
            // ReSharper disable PossibleMultipleEnumeration
            switch (expressions)
            {
                case IReadOnlyList<Expression> list:
                    return ResolveReadOnlyListValues(list);
                case IList<Expression> list:
                    return ResolveListValues(list);
                default:
                    return expressions.Select(ResolveValue).ToArray();
            }
            // ReSharper enable PossibleMultipleEnumeration
        }

        private static object[] ResolveReadOnlyListValues(IReadOnlyList<Expression> expressions)
        {
            var values = new object[expressions.Count];
            for (var i = 0; i < expressions.Count; i++)
                values[i] = expressions[i].ResolveValue();
            return values;
        }

        private static object[] ResolveListValues(IList<Expression> expressions)
        {
            var values = new object[expressions.Count];
            for (var i = 0; i < expressions.Count; i++)
                values[i] = expressions[i].ResolveValue();
            return values;
        }

        public static object ResolveValue(Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            
            switch (expression.NodeType)
            {
                case ExpressionType.Parameter:
                    throw new ArgumentException(ExpressionNotSupported, nameof(expression));
                case ExpressionType.Constant:
                    return ((ConstantExpression) expression).Value;
                case ExpressionType.MemberAccess:
                    return ResolveMemberAccessValue((MemberExpression) expression);
                case ExpressionType.New:
                    return ResolveNewValue((NewExpression) expression);
                case ExpressionType.MemberInit:
                    return ResolveMemberInitValue((MemberInitExpression) expression);
                case ExpressionType.Call:
                    return ResolveMethodCallValue((MethodCallExpression) expression);
                case ExpressionType.ArrayLength:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                case ExpressionType.UnaryPlus:
                    return ResolveValueFromUnaryExpression((UnaryExpression) expression);
                case ExpressionType.TypeIs:
                    return ResolveValueFromTypeBinaryExpression((TypeBinaryExpression) expression);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Power:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return ResolveValueFromBinaryArithmeticExpression((BinaryExpression) expression);
                case ExpressionType.And:
                    return ResolveValueFromBitExpression((BinaryExpression) expression, (x, y) => x & y);
                case ExpressionType.Or:
                    return ResolveValueFromBitExpression((BinaryExpression) expression, (x, y) => x | y);
                case ExpressionType.ExclusiveOr:
                    return ResolveValueFromBitExpression((BinaryExpression) expression, (x, y) => x ^ y);
                case ExpressionType.LeftShift:
                    return ResolveValueFromBitExpression((BinaryExpression) expression, (x, y) => x << y);
                case ExpressionType.RightShift:
                    return ResolveValueFromBitExpression((BinaryExpression) expression, (x, y) => x >> y);
                case ExpressionType.AndAlso:
                    return ResolveValueFromConditionalExpression((BinaryExpression) expression, (x, y) => x && y);
                case ExpressionType.OrElse:
                    return ResolveValueFromConditionalExpression((BinaryExpression) expression, (x, y) => x || y);
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                    return ResolveValueFromComparisonExpression((BinaryExpression) expression);
                case ExpressionType.Coalesce:
                    return ResolveValueFromCoalesceExpression((BinaryExpression) expression);
                case ExpressionType.ArrayIndex:
                    return ResolveValueFromArrayIndexExpression((BinaryExpression) expression);
                case ExpressionType.Default:
                    return ResolveValueFromDefaultExpression((DefaultExpression) expression);
                default:
                    return ResolveCompiledValue(expression);
            }
        }

        private static object ResolveValueFromDefaultExpression(DefaultExpression expression)
        {
            var type = expression.Type;
            if (type.GetTypeInfo().IsValueType)
                return Activator.CreateInstance(type);
            return null;
        }

        private static object ResolveValueFromArrayIndexExpression(BinaryExpression expression)
        {
            if (expression.NodeType != ExpressionType.ArrayIndex)
                throw new ArgumentException(ExpressionNotSupported, nameof(expression));

            var left = expression.Left.ResolveValue();
            var right = expression.Right.ResolveValue();

            var array = left as Array;
            if (array == null)
                throw new ArgumentException("The left operand of the expression does not represent an instance of Array.", nameof(expression));
            
            var resolvedValue = array.GetValue((int) right);
            return resolvedValue;
        }

        private static object ResolveValueFromCoalesceExpression(BinaryExpression expression)
        {
            if (expression.NodeType != ExpressionType.Coalesce)
                throw new ArgumentException(ExpressionNotSupported, nameof(expression));

            var resolvedValued = expression.Left.ResolveValue() ?? expression.Right.ResolveValue();
            return resolvedValued;
        }

        private static object ResolveValueFromComparisonExpression(BinaryExpression expression)
        {
            var left = expression.Left.ResolveValue();
            var right = expression.Right.ResolveValue();
            return ResolveValueFromComparisonExpression(expression, left, right);
        }

        private static object ResolveValueFromComparisonExpression(
            BinaryExpression expression, 
            object left,
            object right)
        {    
            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                    return expression.ResolveValueFromEqualsExpression(left, right);
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                    return expression.ResolveValueFromComparableExpression(left, right);
                default:
                    throw new ArgumentException(ExpressionNotSupported, nameof(expression));
            }
        }

        private static object ResolveValueFromEqualsExpression(
            this BinaryExpression expression,
            object left,
            object right)
        {
            var method = expression.Method;
            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    return method == null ? Equals(left, right) : method.Invoke(left, new[] {right});
                case ExpressionType.NotEqual:
                    return method == null ? !Equals(left, right) : method.Invoke(left, new[] {right});
                default:
                    throw new ArgumentException(ExpressionNotSupported, nameof(expression));
            }
        }

        private static object ResolveValueFromComparableExpression(
            this BinaryExpression expression, 
            object left,
            object right)
        {
            var method = expression.Method;
            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    return method == null ? ((IComparable) left).CompareTo(right) == 0 : method.Invoke(left, new[] {right});
                case ExpressionType.NotEqual:
                    return method == null ? ((IComparable) left).CompareTo(right) != 0 : method.Invoke(left, new[] {right});
                case ExpressionType.GreaterThanOrEqual:
                    return method == null ? ((IComparable) left).CompareTo(right) >= 0 : method.Invoke(left, new[] {right});
                case ExpressionType.GreaterThan:
                    return method == null ? ((IComparable) left).CompareTo(right) > 0 : method.Invoke(left, new[] {right});
                case ExpressionType.LessThanOrEqual:
                    return method == null ? ((IComparable) left).CompareTo(right) <= 0 : method.Invoke(left, new[] {right});
                case ExpressionType.LessThan:
                    return method == null ? ((IComparable) left).CompareTo(right) < 0 : method.Invoke(left, new[] {right});
                default:
                    throw new ArgumentException(ExpressionNotSupported, nameof(expression));
            }
        }

        private static object ResolveValueFromConditionalExpression(BinaryExpression expression,
            Func<bool, bool, bool> operation)
        {
            if (expression.NodeType != ExpressionType.AndAlso && expression.NodeType != ExpressionType.OrElse)
                throw new ArgumentException(ExpressionNotSupported, nameof(expression));

            var left = expression.Left.ResolveValue();
            var right = expression.Right.ResolveValue();
            var resolvedValue = operation((bool) left, (bool) right);
            return resolvedValue;
        }

        private static object ResolveValueFromBitExpression(
            this BinaryExpression expression,
            Func<int, int, int> operation)
        {
            var left = expression.Left.ResolveValue();
            var right = expression.Right.ResolveValue();
            var resolvedValue = operation((int) left, (int) right);
            return resolvedValue;
        }

        private static object ResolveValueFromBinaryArithmeticExpression(
            this BinaryExpression expression)
        {
            var left = expression.Left.ResolveValue();
            var right = expression.Right.ResolveValue();

            if (expression.Method != null)
                return expression.Method.Invoke(null, new []{left, right});

            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                    switch (left)
                    {
                        case long value:
                            return unchecked(value + (long) right);
                        case int value:
                            return unchecked(value + (int) right);
                        case short value:
                            return unchecked(value + (short) right);
                        case byte value:
                            return unchecked(value + (byte) right);
                        case float value:
                            return value + (float) right;
                        case double value:
                            return value + (double) right;
                    }
                    break;
                case ExpressionType.AddChecked:
                    switch (left)
                    {
                        case long value:
                            return checked(value + (long) right);
                        case int value:
                            return checked(value + (int) right);
                        case short value:
                            return checked(value + (short) right);
                        case byte value:
                            return checked(value + (byte) right);
                        case float value:
                            return value + (float) right;
                        case double value:
                            return value + (double) right;
                    }
                    break;
                case ExpressionType.Divide:
                    switch (left)
                    {
                        case long value:
                            return value / (long) right;
                        case int value:
                            return value / (int) right;
                        case short value:
                            return value / (short) right;
                        case byte value:
                            return value / (byte) right;
                        case float value:
                            return value / (float) right;
                        case double value:
                            return value / (double) right;
                    }
                    break;
                case ExpressionType.Modulo:
                    switch (left)
                    {
                        case long value:
                            return value % (long) right;
                        case int value:
                            return value % (int) right;
                        case short value:
                            return value % (short) right;
                        case byte value:
                            return value % (byte) right;
                        case float value:
                            return value % (float) right;
                        case double value:
                            return value % (double) right;
                    }
                    break;
                case ExpressionType.Multiply:
                    switch (left)
                    {
                        case long value:
                            return unchecked(value * (long) right);
                        case int value:
                            return unchecked(value * (int) right);
                        case short value:
                            return unchecked(value * (short) right);
                        case byte value:
                            return unchecked(value * (byte) right);
                        case float value:
                            return value * (float) right;
                        case double value:
                            return value * (double) right;
                    }
                    break;
                case ExpressionType.MultiplyChecked:
                    switch (left)
                    {
                        case long value:
                            return checked(value * (long) right);
                        case int value:
                            return checked(value * (int) right);
                        case short value:
                            return checked(value * (short) right);
                        case byte value:
                            return checked(value * (byte) right);
                        case float value:
                            return value * (float) right;
                        case double value:
                            return value * (double) right;
                    }
                    break;
                case ExpressionType.Subtract:
                    switch (left)
                    {
                        case long value:
                            return unchecked(value - (long) right);
                        case int value:
                            return unchecked(value - (int) right);
                        case short value:
                            return unchecked(value - (short) right);
                        case byte value:
                            return unchecked(value - (byte) right);
                        case float value:
                            return value - (float) right;
                        case double value:
                            return value - (double) right;
                    }
                    break;
                case ExpressionType.SubtractChecked:
                    switch (left)
                    {
                        case long value:
                            return checked(value - (long) right);
                        case int value:
                            return checked(value - (int) right);
                        case short value:
                            return checked(value - (short) right);
                        case byte value:
                            return checked(value - (byte) right);
                        case float value:
                            return value - (float) right;
                        case double value:
                            return value - (double) right;
                    }
                    break;
                case ExpressionType.Power:
                    // Power expressions are not natively supported in C#
                    // We could make a fast-path by using Math.Pow, but is anyone really going to use it?
                    return ResolveCompiledValue(expression);
            }

            throw new ArgumentException(ExpressionNotSupported, nameof(expression));
        }
        
        private static object ResolveValueFromTypeBinaryExpression(TypeBinaryExpression expression)
        {
            var operand = expression.Expression.ResolveValue();
            var typeOperand = expression.TypeOperand;
            var testResult = typeOperand.GetTypeInfo().IsInstanceOfType(operand);
            return testResult;
        }

        private static object ResolveValueFromUnaryExpression(UnaryExpression expression)
        {
            var value = expression.Operand.ResolveValue();

            if (expression.IsLiftedToNull && value == null)
                return null;

            if (expression.Method != null)
                return expression.Method.Invoke(value, new[] {value});

            switch (expression.NodeType)
            {
                case ExpressionType.ArrayLength:
                    return ((Array) value).Length;
                case ExpressionType.Convert:
                    try
                    {
                        var type = expression.IsLiftedToNull
                            ? Nullable.GetUnderlyingType(expression.Type)
                            : expression.Type;
                        
                        // ReSharper disable once AssignNullToNotNullAttribute
                        return Convert.ChangeType(value, type);
                    }
                    catch (OverflowException)
                    {
                        return ResolveCompiledValue(expression);
                    }
                case ExpressionType.ConvertChecked:
                    {
                        var type = expression.IsLiftedToNull
                            ? Nullable.GetUnderlyingType(expression.Type)
                            : expression.Type;

                        // ReSharper disable once AssignNullToNotNullAttribute
                        return Convert.ChangeType(value, type);
                    }
                case ExpressionType.Negate:
                {
                    switch (value)
                    {
                        case byte v: return -v;
                        case short v: return -v;
                        case int v: return -v;
                        case long v: return -v;
                        case float v: return -v;
                        case double v: return -v;
                        default:
                            throw new ArgumentException();
                    }
                }
                case ExpressionType.NegateChecked:
                {
                    switch (value)
                    {
                        case byte v: return checked(-v);
                        case short v: return checked(-v);
                        case int v: return checked(-v);
                        case long v: return checked(-v);
                        case float v: return -v;
                        case double v: return -v;
                        default:
                            throw new ArgumentException();
                    }
                }
                case ExpressionType.Not:
                    return !(bool) value;
                case ExpressionType.Quote:
                    return ResolveValue(expression.Operand);
                case ExpressionType.TypeAs:
                {
                    if (value == null)
                        return null;
                    
                    var type = expression.IsLiftedToNull
                        ? Nullable.GetUnderlyingType(expression.Type)
                        : expression.Type;
                    
                    return Convert.ChangeType(value, type);
                }
                case ExpressionType.UnaryPlus:
                    // If a method is not defined for this conversion, the default behaviour is to resolve to the value.
                    return value;
                default:
                    throw new ArgumentException(ExpressionNotSupported, nameof(expression));
            }
        }

        private static object ResolveMethodCallValue(MethodCallExpression expression)
        {
            var source = expression.Object.ResolveValue();
            var arguments = ResolveValues(expression.Arguments);
            var returnValue = expression.Method.Invoke(source, arguments);
            return returnValue;
        }

        private static object ResolveCompiledValue(Expression expression)
        {
            var valueConversion = Expression.Convert(expression, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(valueConversion);
            var valueResolver = lambda.Compile();
            var value = valueResolver();
            return value;
        }

        private static object ResolveMemberInitValue(MemberInitExpression expression)
        {
            var instance = ResolveNewValue(expression.NewExpression);
            foreach (var binding in expression.Bindings)
            {
                AssignMember(instance, binding);
            }
            return instance;
        }

        private static void AssignMember(object instance, MemberBinding binding)
        {
            switch (binding.BindingType)
            {
                case MemberBindingType.Assignment:
                    AssignMember(instance, (MemberAssignment) binding);
                    break;
                case MemberBindingType.MemberBinding:
                    AssignMember(instance, (MemberMemberBinding) binding);
                    break;
                case MemberBindingType.ListBinding:
                    AssignMember(instance, (MemberListBinding) binding);
                    break;
            }
        }

        private static void AssignMember(object instance, MemberAssignment assignment)
        {
            var value = assignment.Expression.ResolveValue();
            switch (assignment.Member.MemberType)
            {
                case MemberTypes.Property:
                    ((PropertyInfo) assignment.Member).SetValue(instance, value);
                    break;
                case MemberTypes.Field:
                    ((FieldInfo) assignment.Member).SetValue(instance, value);
                    break;
                default:
                    throw new ArgumentException("Only property and field assignments are supported.", nameof(assignment));
            }
        }

        private static void AssignMember(object instance, MemberMemberBinding assignment)
        {
            foreach (var binding in assignment.Bindings)
            {
                AssignMember(instance, binding);
            }
        }

        private static void AssignMember(object instance, MemberListBinding assignment)
        {
            foreach (var init in assignment.Initializers)
            {
                var arguments = new object[init.Arguments.Count];
                for (var i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = init.Arguments[i]?.ResolveValue();
                }

                init.AddMethod.Invoke(instance, arguments);
            }
        }

        private static object ResolveNewValue(NewExpression expression)
        {
            var arguments = ResolveValues(expression.Arguments);
            var instance = expression.Constructor.Invoke(arguments);
            return instance;
        }

        private static object ResolveMemberAccessValue(MemberExpression expression)
        {
            var source = expression.Expression?.ResolveValue();
            var member = expression.Member;
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo) member).GetValue(source);
                case MemberTypes.Property:
                    return ((PropertyInfo) member).GetValue(source);
                default:
                    return ResolveCompiledValue(expression);
            }
        }
    }
}