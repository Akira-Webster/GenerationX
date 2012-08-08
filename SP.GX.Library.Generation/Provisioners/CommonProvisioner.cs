using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.SharePoint.Common.Logging;
using Microsoft.SharePoint.Administration;

namespace SP.GX.Library.Generation.Provisioners
{
    public abstract class CommonProvisioner<Keys, Child> where Child : class
    {
        public CommonProvisioner()
        {
            this.Cache = new Hashtable();

            this.Rules = new Dictionary<Keys, List<BaseRule<Child>>>();
        }

        #region ServiceLocator

        public virtual Microsoft.Practices.ServiceLocation.IServiceLocator ServiceLocator { get { return null; } }

        #endregion

        #region Rules

        public Dictionary<Keys, List<BaseRule<Child>>> Rules { get; private set; }

        protected void ExecuteRules(Keys key)
        {
            var rules = this.Rules[key];

            int index = 0;
            try
            {
                foreach (var rule in rules)
                {
                    rule.Execute();
                    index++;
                }
            }
            catch (Exception ex)
            {
                // rollback rule in reverse from the index that failed
                for (int i = index; i > -1; i--)
                {
                    try
                    {
                        rules[i].RollBack();
                    }
                    catch (Exception)
                    {
                    }
                }

                throw ex;
            }
        }

        protected virtual void AddRule(Keys key, BaseRule<Child> rule)
        {
            this.Rules[key].Add(rule);
        }

        protected virtual void AddRule(Keys key, Action execute, Action rollback)
        {
            var rule = new RuleByActions<Child>(this as Child, execute, rollback);
            this.Rules[key].Add(rule);
        }

        protected virtual void AddRule(Keys key, Action execute)
        {
            var rule = new RuleByActions<Child>(this as Child, execute, () => {});
            this.Rules[key].Add(rule);
        }

        #endregion

        #region Logging

        public virtual void Log(Exception ex, int eventID)
        {
            IServiceLocator serviceLocator = this.ServiceLocator;
            if (serviceLocator == null)
                return;

            var logger = serviceLocator.GetInstance<ILogger>();

            if (logger == null)
                return;

            logger.TraceToDeveloper(ex, eventID, SandboxTraceSeverity.Unexpected, "Akira");
        }

        public virtual void Log(Exception ex)
        {
            IServiceLocator serviceLocator = this.ServiceLocator;
            if (serviceLocator == null)
                return;

            var logger = serviceLocator.GetInstance<ILogger>();

            if (logger == null)
                return;

            logger.TraceToDeveloper(ex, 1, SandboxTraceSeverity.Unexpected, "Akira");
        }

        public virtual void Log(string message, int eventID)
        {
            IServiceLocator serviceLocator = this.ServiceLocator;
            if (serviceLocator == null)
                return;

            var logger = serviceLocator.GetInstance<ILogger>();

            if (logger == null)
                return;

            logger.TraceToDeveloper(message, 1, SandboxTraceSeverity.Verbose, "Akira");
        }

        public virtual void Log(string message)
        {
            IServiceLocator serviceLocator = this.ServiceLocator;
            if (serviceLocator == null)
                return;

            var logger = serviceLocator.GetInstance<ILogger>();

            if (logger == null)
                return;

            logger.TraceToDeveloper(message, 1, SandboxTraceSeverity.Verbose, "Akira");
        }

        #endregion

        #region Caching

        private Hashtable Cache = null;

        public void SetProperty<T>(string key, T obj)
        {
            this.Cache[key] = obj;
        }

        public T GetProperty<T>(string key)
        {
            try
            {
                return (T)this.Cache[key];
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public bool HasProperty(string key)
        {
            return this.Cache.ContainsKey(key);
        }

        #endregion
    }
}
