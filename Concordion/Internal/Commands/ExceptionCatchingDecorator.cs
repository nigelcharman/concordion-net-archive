﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Concordion.Api;
using System.Threading;

namespace Concordion.Internal.Commands
{
    public class ExceptionCatchingDecorator : AbstractCommandDecorator
    {
        #region Constructors
        
        public ExceptionCatchingDecorator(ICommand command)
            : base(command)
        {
        } 

        #endregion

        #region Methods
        
        private void OnExceptionCaught(Element element, Exception exception, string expression)
        {
            if (ExceptionCaught != null)
            {
                ExceptionCaught(this, new ExceptionCaughtEventArgs { Element = element, Exception = exception, Expression = expression });
            }
        } 

        #endregion

        #region Override Methods

        public override void SetUp(CommandCall commandCall, IEvaluator evaluator, IResultRecorder resultRecorder)
        {
            try
            {
                m_command.SetUp(commandCall, evaluator, resultRecorder);
            }
            catch (Exception e)
            {
                resultRecorder.Record(Result.Exception);
                OnExceptionCaught(commandCall.Element, e, commandCall.Expression);
            }
        }

        public override void Execute(CommandCall commandCall, IEvaluator evaluator, IResultRecorder resultRecorder)
        {
            try
            {
                m_command.Execute(commandCall, evaluator, resultRecorder);
            }
            catch (Exception e)
            {
                resultRecorder.Record(Result.Exception);
                OnExceptionCaught(commandCall.Element, e, commandCall.Expression);
            }
        }

        public override void Verify(CommandCall commandCall, IEvaluator evaluator, IResultRecorder resultRecorder)
        {
            try
            {
                m_command.Verify(commandCall, evaluator, resultRecorder);
            }
            catch (Exception e)
            {
                resultRecorder.Record(Result.Exception);
                OnExceptionCaught(commandCall.Element, e, commandCall.Expression);
            }
        }

        #endregion

        #region Events
        
        public event EventHandler<ExceptionCaughtEventArgs> ExceptionCaught; 

        #endregion
    }
}