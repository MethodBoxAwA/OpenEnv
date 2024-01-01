/*
 * @Author 拟南芥
 * @Time 2023/12/31
 * @Description 项目主窗口
 */

using Sunny.UI;
using System.Linq.Expressions;

namespace OpenEnv
{
    public partial class Main : UIForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Expression<Func<int, int, int>> expression = (x, y) => x+y;
            expression.Compile()(1, 2).ToString();
        }
    }
}
