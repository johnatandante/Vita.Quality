using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Utilities.Statement
{    

    public class Statement
    {
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
            public static Op Under { get { return new Op("Under"); } }
       
            Op(string stringOp) : base(stringOp) { }


        }

        public sealed class Clause : BaseOp
        {
            public static Clause And { get { return new Clause("AND"); } }
            public static Clause Or { get { return new Clause("OR"); } }

            

            Clause(string stringOp) : base(stringOp) { }

        }
        
        public Statement(string op)
        {
            Operator = op;
        }

        public readonly string Operator;

        public readonly List<StatementItem> Clauses = new List<StatementItem>();

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

            foreach (StatementItem item in Clauses)
            {
                if (Operator == "AND")
                {
                    result = result.And(item.ToString());
                }
                else if(string.IsNullOrEmpty(item.ToString()))
                {
                    result = string.Join(Operator, result, item.ToString());

                }
                // else continue
            }

            return result;
        }

        public Statement Where(string name, string value, Op op = null)
        {            

            Clauses.Add(new StatementItem(name, value, op ?? Op.Uguale));

            return this;
        }

        public Statement WhereNot(string name, string value)
        {
            Clauses.Add(new StatementItem(name, value, Op.Diverso));

            return this;
        }

    }

    public class StatementItem
    {

        public readonly string Name;
        public readonly object Value;
        public readonly Statement.Op Operator;
        
        public StatementItem(string name, string value, Statement.Op op = null)
        {
            Name = name;
            Value = value;
            Operator = op ?? Statement.Op.Uguale;
        }

        public override sealed string ToString()
        {
            return string.Join(Operator.ToString(), Name, Value);
        }

    }
    
}
