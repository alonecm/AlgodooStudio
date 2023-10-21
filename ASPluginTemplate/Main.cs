using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlgodooStudio.Base;
using AlgodooStudio.PluginSystem;

namespace ASPluginTemplate
{
    /// <summary>
    /// 插件本体
    /// </summary>
    public sealed class Main : Plugin
    {
        public Main()
        {
            base.id = 0;
            base.name = "PluginTemplate";
            base.version = "1.0.0";
        }

        public override void OnLoad()
        {

        }

        public override void OnEnabled()
        {

        }

        public override void OnDisabled()
        {

        }

        public override void OnUnload()
        {

        }
    }
}
