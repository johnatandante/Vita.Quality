using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Utilities.Statement
{    

    public class Statement
    {

        public static BaseOp OrderByStatement = new QueryStatement( "order by");

        class QueryStatement : BaseOp
        {
            public QueryStatement(string op) : base(op) { }
        }

        public class BaseOp
        {
            string _stringOp;
            protected BaseOp(string stringOp) {
                _stringOp = stringOp;
            }

            public override sealed string ToString()
            {
                return _stringOp;
            }
        }

        public sealed class Op : BaseOp
        {
            public static Op Uguale { get { return new Op("="); } }
            public static Op Diverso { get { return new Op("<>"); } }
            public static Op Under { get { return new Op("under"); } }
            public static Op Contains { get { return new Op("contains"); } }
            
            Op(string stringOp) : base(stringOp) { }
            
        }

        public sealed class Order : BaseOp
        {
            public static Order Ascending { get { return new Order("asc"); } }
            public static Order Descending { get { return new Order("desc"); } }

            Order(string stringOp) : base(stringOp) { }

        }

        public sealed class Clause : BaseOp
        {
            public static Clause And { get { return new Clause("and"); } }
            public static Clause Or { get { return new Clause("or"); } }

            Clause(string stringOp) : base(stringOp) { }

        }
        
        public Statement(string op)
        {
            Operator = op;
        }

        public readonly string Operator;

        readonly List<StatementItem> Clauses = new List<StatementItem>();

        readonly List<StatementItem> OrderClauses = new List<StatementItem>();

        public static Statement New(string op = "AND")
        {
            return new Statement(op);
        }

        /// <summary>
        /// 1 level only
        /// </summary>
        public override sealed string ToString()
        {
            string result = string.Empty;

            string clauses = string.Empty;
            foreach (StatementItem item in Clauses)
            {
                if (Operator == "AND")
                {
                    clauses = clauses.And(item.ToString());
                }
                else if (string.IsNullOrEmpty(item.ToString()))
                {
                    clauses = string.Join(Operator, clauses, item.ToString());

                }
                // else continue
            }

            string orderby = string.Empty;

            if(OrderClauses.Any())
            {
                orderby = string.Join( " ", OrderByStatement.ToString(),
                    string.Join(", ", OrderClauses.Select( item => item.ToString())));

            }
            
            return string.Join(" ", clauses, orderby);

        }

        public Statement Where(Enum item, string value, Op op = null)
        {
            return Where(item.FieldName(), value, op);
        }

        public Statement Where(string name, string value, Op op = null)
        {            

            Clauses.Add(new WhereStatementItem(name.Bracketed(), value.StartsWith("@") ? value : value.Quoted(), op ?? Op.Uguale));

            return this;
        }

        public Statement WhereNot(Enum item, string value)
        {
            return WhereNot(item.FieldName(), value);
        }

        public Statement WhereNot(string name, string value)
        {
            Clauses.Add(new WhereStatementItem(name.Bracketed(), value.Quoted(), Op.Diverso));

            return this;
        }

        public Statement OrderBy(Enum item, Order direction = null)
        {
            return OrderBy(item.FieldName(), direction);
        }

        public Statement OrderBy(string name, Order direction = null)
        {

            OrderClauses.Add(new OrderByStatementItem(name.Bracketed(), direction ?? Order.Ascending));
            
            return this;
        }
    }

    abstract class StatementItem
    {
        public readonly string Name;
        public readonly object Value;
        public readonly Statement.BaseOp Operator;
        
        public StatementItem(string name, object value, Statement.BaseOp op)
        {
            Name = name;
            Value = value;
            Operator = op;
        }

    }

    class OrderByStatementItem : StatementItem
    {
        public OrderByStatementItem(string name, Statement.Order op = null)
            : base(name, (op ?? Statement.Order.Ascending), Statement.OrderByStatement) { }

        public override sealed string ToString()
        {
            return string.Join(" ", Name, Value);
        }

    }

    class WhereStatementItem : StatementItem
    {
        
        public WhereStatementItem(string name, string value, Statement.Op op = null)
             : base(name, value, op ?? Statement.Op.Uguale) { }
        
        public override sealed string ToString()
        {
            return string.Join(" " + Operator.ToString() + " ", Name, Value);
        }

    }
    
}
