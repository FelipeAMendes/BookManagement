using System.Linq.Expressions;

namespace BookManagement.Core.Shared.Extensions.ExpressionExtensions;

public static class ExpressionExtensions
{
    public static string GetMemberName(this Expression expression)
    {
        return expression switch
        {
            null => throw new ArgumentException("Invalid"),
            MemberExpression memberExpression => memberExpression.Member.Name,
            MethodCallExpression methodCallExpression => methodCallExpression.Method.Name,
            UnaryExpression unaryExpression => GetMemberName(unaryExpression),
            _ => throw new ArgumentException("Invalid")
        };
    }

    private static string GetMemberName(UnaryExpression unaryExpression)
    {
        return unaryExpression.Operand is MethodCallExpression methodExpression
            ? methodExpression.Method.Name
            : ((MemberExpression) unaryExpression.Operand).Member.Name;
    }
}