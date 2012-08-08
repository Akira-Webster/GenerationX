using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.GX.Library.Generation.Provisioners
{
    public abstract class BaseRule<T>
    {
        public T Provisioner { get; private set; }

        public BaseRule(T p)
        {
            this.Provisioner = p;
        }

        public virtual void Execute()
        {
        }

        public virtual void RollBack()
        {
        }
    }

    public class RuleByActions<T> : BaseRule<T>
    {
        private Action excutePath;
        private Action rollbackPath;

        public RuleByActions(T p, Action excute)
            : base(p)
        {
            this.excutePath = excute;
        }

        public RuleByActions(T p, Action excute, Action rollBack)
            : base(p)
        {
            this.excutePath = excute;
            this.rollbackPath = rollBack;
        }

        public override void Execute()
        {
            if(this.excutePath != null)
                this.excutePath();
        }

        public override void RollBack()
        {
            if (this.rollbackPath != null)
                this.rollbackPath();
        }
    }
}
