﻿using System;
using System.Collections.Generic;
using Moviebase.Core.Diagnostics;

namespace Moviebase.Core
{
    public class ValidationSupport
    {
        private Dictionary<Func<bool>, string> _conditionDictionary;
        private Action<string> _failAction;
        
        public ValidationSupport()
        {
            _conditionDictionary = new Dictionary<Func<bool>, string>();
        }

        public ValidationSupport IsTrue(Func<bool> condition, string failMessage)
        {
            _conditionDictionary.Add(condition, failMessage);
            return this;
        }

        public ValidationSupport SetCommonFailAction(Action<string> msgBox)
        {
            _failAction = msgBox;
            return this;
        }

        public bool Validate()
        {
            foreach (var condition in _conditionDictionary)
            {
                if (condition.Key.Invoke()) continue;
                _failAction.Invoke(condition.Value);
                return false;
            }
            return true;
        }

        public static bool QuickTrue(Func<bool> condition, string failLog)
        {
            var result = condition.Invoke();
            return result;
        }
    }
}
