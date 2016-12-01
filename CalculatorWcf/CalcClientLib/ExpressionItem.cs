﻿using System.Collections.Generic;

namespace CalcClientLib
{
    public class ExpressionItem
    {
        public int Priority { get; protected set; }
        public int StackPriority { get; protected set; }
        public bool isUnary { get; protected set; } = false;
    }

    public class Operation : ExpressionItem
    {
        public static readonly Operation Add = new Operation() { Priority = 1, StackPriority = 2 };
        public static readonly Operation Substract = new Operation() { Priority = 1, StackPriority = 2 };

        public static readonly Operation Multiply = new Operation() { Priority = 3, StackPriority = 4 };
        public static readonly Operation Divide = new Operation() { Priority = 3, StackPriority = 4 };

        public static readonly Operation Negation = new Operation() { Priority = 20, StackPriority = 21, isUnary = true };
        public static readonly Operation Power = new Operation() { Priority = 14, StackPriority = 13 }; // right association: Priority > StackPriority

        public static readonly Dictionary<char, Operation> BySign =
            new Dictionary<char, Operation>()
        {
            {'+', Add},
            {'-', Substract},
            {'*', Multiply},
            {'/', Divide},
            {'^', Power}
        };

        // Public

        public static bool IsOperator(char sign)
        {
            return BySign.ContainsKey(sign);
        }

        public override string ToString()
        {
            return SignByOp[this];
        }

        // Internal

        private Operation() { }

        private static readonly Dictionary<Operation, string> SignByOp =
            new Dictionary<Operation, string>()
        {
            {Add, "+"},
            {Substract, "-"},
            {Negation, "-"},
            {Multiply, "*"},
            {Divide, "/"},
            {Power, "^" }
        };
    }

    public class Divisor : ExpressionItem
    {
        public static readonly Divisor OpenBracket = new Divisor() { Priority = 9, StackPriority = 0 };
        public static readonly Divisor CloseBracket = new Divisor() { Priority = 0 };   // never pushed into stack

        public static readonly Dictionary<char, Divisor> BySign =
            new Dictionary<char, Divisor>()
        {
            {'(', OpenBracket},
            {')', CloseBracket}
        };

        // Public
        
        public static bool IsDivisor(char c)
        {
            return BySign.ContainsKey(c);
        }

        public override string ToString()
        {
            return SignByDiv[this];
        }

        // Internal

        private Divisor() { }

        private static readonly Dictionary<Divisor, string> SignByDiv =
            new Dictionary<Divisor, string>()
        {
            {OpenBracket, "("},
            {CloseBracket, ")"}
        };
    }

    public class Operand : ExpressionItem   // never pushed into stack
    {
        public double Value { get; set; }

        public Operand(double value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
