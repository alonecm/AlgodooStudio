using AlgodooStudio.Analyzer.Stream;
using AlgodooStudio.Base;
using AlgodooStudio.Phun;
using AlgodooStudio.Phun.Archive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgodooStudio.Analyzer
{
    /// <summary>
    /// Phun存档解析器，用于解析存档中的软件设置和场景物体
    /// </summary>
    internal sealed class PhunAnalyzer
    {
        /// <summary>
        /// 场景设置集合
        /// </summary>
        internal SceneSettings settings = new SceneSettings();
        /// <summary>
        /// 场景物体集合
        /// </summary>
        internal SceneObjects objects = new SceneObjects();
        /// <summary>
        /// 场景对象集合
        /// </summary>
        internal SceneVariables sceneVars = new SceneVariables();
        /// <summary>
        /// 存档解析流
        /// </summary>
        private PhnStream stream;
        /// <summary>
        /// 上一次分析的字符串
        /// </summary>
        private string lastContent;

        /// <summary>
        /// 分析存档
        /// </summary>
        internal void Analyzer(string content)
        {
            //确保不会反复分析
            if (lastContent != content)
            {
                lastContent = content;
                settings.Clear();
                objects.Clear();
                sceneVars.Clear();
                stream = new PhnStream(content);//重创建流
                Container<PhnUnit> units = new Container<PhnUnit>();
                //流读取
                while (!stream.EndOfRead())
                {
                    units.Add(stream.Next());
                }
                //拆分内容
                int id = 0;//物体ID
                foreach (var unit in units)
                {
                    switch (unit.type)
                    {
                        case UnitType.SceneMyVar:
                            sceneVars.Add(new SceneVariable(unit.items[0].name.TrimEnd(' '), unit.items[0].content.TrimStart(' ')));
                            break;
                        case UnitType.SettingGroup:
                            SettingGroup sg = new SettingGroup();
                            sg.GroupName = unit.name.TrimEnd(' ');
                            foreach (var item in unit.items)
                            {
                                sg.Add(new SettingItem(item.name.TrimEnd(' '), item.content.TrimStart(' ')));
                            }
                            settings.Add(sg);
                            break;
                        case UnitType.Object:
                            SceneObject so = new SceneObject(id);
                            so.Value = unit.name.TrimEnd(' ');
                            foreach (var item in unit.items)
                            {
                                so.Items.Add(new ObjectItem(item.name.TrimEnd(' '), item.content.TrimStart(' ')));
                            }
                            objects.Add(so);
                            id++;
                            break;
                    }
                }
            }
        }
    }
}
