using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Interface
{

    /// <summary>
    /// 基本信息接口
    /// </summary>
    public interface IBasicIdentity
    {
        /// <summary>
        /// 编号
        /// </summary>
        int ID { get; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name{ get; }
    }
}
