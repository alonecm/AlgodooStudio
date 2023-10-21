using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Phun
{
    /// <summary>
    /// 场景变量
    /// </summary>
    public sealed class SceneVariable
    {
        internal readonly string name;

        internal readonly string value;

        internal SceneVariable(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
