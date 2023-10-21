using AlgodooStudio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Phun.Archive
{
    /// <summary>
    /// 存档中的场景变量集合
    /// </summary>
    internal sealed class SceneVariables: Container<SceneVariable>
    {
        public SceneVariables(params SceneVariable[] contents) : base(contents)
        {
        }
    }
}
