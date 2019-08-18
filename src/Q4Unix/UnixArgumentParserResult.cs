﻿using System;
using System.Collections.Generic;
using System.Text;
using Qmmands;

namespace Q4Unix
{
    /// <summary>
    ///     Represents the result of a <see cref="UnixArgumentParser"/>.
    /// </summary>
    public class UnixArgumentParserResult : ArgumentParserResult
    {
        internal static UnixArgumentParserResult UnknownParameter(CommandContext context, string rawArguments, IReadOnlyDictionary<Parameter, object> arguments, string parameterName, int position)
        {
            return new UnixArgumentParserResult(context, rawArguments, arguments, UnixArgumentParseFailure.UnknownParameter, $"An argument called \"{parameterName}\" was passed that is not a known parameter.", position);
        }

        internal static UnixArgumentParserResult UnexpectedQuote(CommandContext context, string rawArguments, IReadOnlyDictionary<Parameter, object> arguments, int position)
        {
            return new UnixArgumentParserResult(context, rawArguments, arguments, UnixArgumentParseFailure.UnexpectedQuote,
                "Unexpected quote.", position);
        }

        internal static UnixArgumentParserResult UnclosedQuote(CommandContext context, string rawArguments, IReadOnlyDictionary<Parameter, object> arguments, int position)
        {
            return new UnixArgumentParserResult(context, rawArguments, arguments, UnixArgumentParseFailure.UnclosedQuote,
                "Unclosed quote.", position);
        }

        internal static UnixArgumentParserResult TooFewArguments(CommandContext context, string rawArguments, IReadOnlyDictionary<Parameter, object> arguments, Parameter expectedParameter)
        {
            return new UnixArgumentParserResult(context, rawArguments, arguments, UnixArgumentParseFailure.TooFewArguments,
                $"A parameter of name \"{expectedParameter.Name}\" is missing.", -1, expectedParameter);
        }

        internal static UnixArgumentParserResult Successful(CommandContext context, IReadOnlyDictionary<Parameter, object> arguments)
        {
            return new UnixArgumentParserResult(context, arguments);
        }

        private UnixArgumentParserResult(CommandContext context, string rawArguments, IReadOnlyDictionary<Parameter, object> arguments, UnixArgumentParseFailure failure, string reason, int position, Parameter parameter = null) : base(arguments)
        {
            _reason = reason;
            ParseFailure = failure;
            Position = position;
            RawArguments = rawArguments;
            Expected = parameter;
            IsSuccessful = false;
            Context = context;
        }

        private UnixArgumentParserResult(CommandContext context, IReadOnlyDictionary<Parameter, object> arguments) : base(arguments)
        {
            Context = context;
            IsSuccessful = true;
        }

        private readonly string _reason;

        public override bool IsSuccessful { get; }

        /// <summary>
        ///     The context that the parse failed on.
        /// </summary>
        public CommandContext Context { get; }

        /// <summary>
        ///     The raw arguments that were passed to the <see cref="UnixArgumentParserResult"/>.
        /// </summary>
        public string RawArguments { get; }

        /// <summary>
        ///     The error that occurred during parsing. This will be null if <see cref="IsSuccessful"/> is true.
        /// </summary>
        public UnixArgumentParseFailure ParseFailure { get; }

        /// <summary>
        ///     The expected parameter. This will always be null, unless <see cref="ParseFailure"/> is <see cref="UnixArgumentParseFailure.TooFewArguments"/>.
        /// </summary>
        public Parameter Expected { get; }

        /// <summary>
        ///     The position at which the parsing error occurred. This will be -1 if the <see cref="ParseFailure"/> is <see cref="UnixArgumentParseFailure.TooFewArguments"/>, or <seealso cref="IsSuccessful"/> is true.
        /// </summary>
        public int Position { get; }

        public override string GetFailureReason() => _reason;
    }

    public enum UnixArgumentParseFailure
    {
        /// <summary>
        ///     A parameter key was passed that is not a parameter.
        /// </summary>
        UnknownParameter,

        /// <summary>
        ///     An unexpected quotation mark was reached.
        /// </summary>
        UnexpectedQuote,

        /// <summary>
        ///     A quotation mark was not closed.
        /// </summary>
        UnclosedQuote,

        /// <summary>
        ///     Not enough arguments were passed.
        /// </summary>
        TooFewArguments
    }
}