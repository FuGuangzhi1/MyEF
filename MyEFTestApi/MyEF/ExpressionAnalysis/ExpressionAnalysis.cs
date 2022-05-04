using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace MyEF.ExpressionAnalysis
{
    public class ExpressionAnalysis : ExpressionVisitor
    {
        string parma = string.Empty;
        private Stack<string> _sqlList = new Stack<string>();
        public string GetSql()
        {
            return string.Join(" ", _sqlList).Replace(char.Parse(parma), ' ');
        }
        [return: NotNullIfNotNull("node")]
        public override Expression? Visit(Expression? node)
        {
            return base.Visit(node);
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            return base.VisitConstant(node);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            parma = node.ToString();
            return base.VisitParameter(node);
        }
        //testInt 和 值
        protected override Expression VisitBinary(BinaryExpression node)
        {
            _sqlList.Push(node.Right.ToString());
            if (node.NodeType == ExpressionType.Equal)
                _sqlList.Push("=");
            _sqlList.Push(node.Left.ToString().Replace('.', ' '));
            return base.VisitBinary(node);
        }

    }
}
