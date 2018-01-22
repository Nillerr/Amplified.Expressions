# Amplified.Expressions

Static expression helpers and extensions with emphasis on fast expression interpretation.

## Usage

```c#
var intValue = 5;
var stringValue = ExpressionHelper.ResolveValue(() => o.ToString());
// stringValue = "5"

var expression = Expression.Constant(5);
var value = ExpressionHelper.ResolveValue(expression);
// value = 5
```

### Advanced Examples

```c#
object[] GetArguments(Expression<Func<object>> expression) {
    var methodCall = expression as MethodCallExpression;
    if (methodCall == null)
        throw new ArgumentException("Expression must be a method call.", nameof(expression));
    
    return methodCall.Arguments.Select(ExpressionHelper.ResolveValue).ToArray();
}

var arguments = GetArguments(() => object.Equals(1, "Hello, World!"));
// arguments = new object[2]{ 1, "Hello, World!" }
// The call to object.Equals(x, y) is not invoked.
```