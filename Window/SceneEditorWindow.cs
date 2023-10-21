using Dex.Canvas;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AlgodooStudio.Window
{
    public partial class SceneEditorWindow : DockContent
    {
        public SceneEditorWindow()
        {
            InitializeComponent();
            Scene s = new Scene();
            Box a = new Box(s.Layers[0], Color.Aqua, new PointF(-100, 0), new SizeF(10, 10), "A");
            Box b = new Box(s.Layers[0], Color.Bisque, new PointF(0, 0), new SizeF(10, 10), "B");
            Box c = new Box(s.Layers[0], Color.Wheat, new PointF(100, 0), new SizeF(10, 10), "C");
            s.AddObjects(0, a);
            s.AddObjects(0, b);
            s.AddObjects(0, c);
            sceneEditor1.CurrentScene = s;
        }

        private void SceneEditorWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            sceneEditor1.Dispose();
        }
    }
}
