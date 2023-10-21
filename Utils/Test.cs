using System;
using System.Diagnostics;

namespace AlgodooStudio.Utils
{
    /// <summary>
    /// 测试用类，提供基本的测试方法
    /// </summary>
    public class Test
    {
        /// <summary>
        /// 测试类停表
        /// </summary>
        private Stopwatch s = new Stopwatch();

        /// <summary>
        /// 测试类独立停表
        /// </summary>
        private static Stopwatch sw = new Stopwatch();

        /// <summary>
        /// 被测试方法
        /// </summary>
        private Delegate @delegate;

        /// <summary>
        /// 被测试方法
        /// </summary>
        public Delegate Delegate { get => @delegate; set => @delegate = value; }

        /// <summary>
        /// 由指定方法创建构造函数
        /// </summary>
        /// <param name="delegate"></param>
        public Test(Delegate @delegate)
        {
            this.@delegate = @delegate;
        }

        /// <summary>
        /// 创建一个空测试
        /// </summary>
        public Test()
        { }

        /// <summary>
        /// 运行时间测试方法
        /// </summary>
        /// <returns>测量的到的运行时间</returns>
        public float TimeTest(params object[] args)
        {
            s.Reset();
            s.Start();
            @delegate.DynamicInvoke(args);
            s.Stop();
            return (float)s.ElapsedTicks / Stopwatch.Frequency * 1000;
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        public static void Start()
        {
            sw.Reset();
            sw.Start();
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        /// <returns>计时时长</returns>
        public static float Stop()
        {
            return (float)sw.ElapsedTicks / Stopwatch.Frequency * 1000;
        }
    }
}