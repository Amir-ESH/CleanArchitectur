using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Common.Helper;

public static class StringToLambda
{
    public static Expression<Func<T, bool>> ParsingToLambda<T>(this string lambda)
    {
        if(string.IsNullOrWhiteSpace(lambda))
            throw new ArgumentNullException(nameof(lambda));

        lambda = lambda.Replace("\n", "").Replace("\r", "");

        var config = new ParsingConfig();

        return DynamicExpressionParser.ParseLambda<T, bool>(config, false, lambda);
    }
}
