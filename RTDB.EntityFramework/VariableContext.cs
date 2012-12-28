using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.EntityFramework
{
    public class VariableContext : DbContext, IVariableContext, IConvention 
    {

        #region 变量集合和组集合

        /// <summary>
        /// 变量组集合
        /// </summary>
        public IDbSet<VariableGroup> VariableGroupSet { get; set; }

        /// <summary>
        /// 数字变量集合
        /// </summary>
        public IDbSet<DigitalVariable> DigitalSet { get; set; }

        /// <summary>
        /// 模拟变量集合
        /// </summary>
        public IDbSet<AnalogVariable> AnalogSet { get; set; }

        /// <summary>
        /// 字符变量集合
        /// </summary>
        public IDbSet<TextVariable> TextSet { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 变量实体集构造函数
        /// </summary>
        /// <param name="dbNameOrConnectingString">数据库名称或者数据库连接字符串</param>
        /// <param name="isLoadData">是否添加数据库值，默认添加</param>
        public VariableContext(string dbNameOrConnectingString = "VariableDB", bool isLoadData = true)
            : base(dbNameOrConnectingString)
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<VariableContext>());
            //加载现有集合及变量
            if (isLoadData)
            {
                LoadVariable();
            }
        }

        #endregion

        #region 加载数据库资料到set集合

        /// <summary>
        /// 数据库加载组信息以及变量信息
        /// </summary>
        private void LoadVariable()
        {
            //遍历数据库变量组数据到set集合
            
            VariableGroupSet.Load();
            DigitalSet.Load();
            AnalogSet.Load();
            TextSet.Load();

            VariableGroup rootGroup = VariableGroupSet.FirstOrDefault(root => root.Parent == null);
            //没有找到根组则添加根组到数据库并返回
            if (rootGroup == null)
            {
                VariableGroupSet.Add(VariableGroup.RootGroup);
                SaveChanges();
                return;
            }
            VariableGroup.RootGroup = VariableGroupSet.Local[0];

            //同步ChildVariables
            synChildVariables(VariableGroup.RootGroup);
        }

        private void synChildVariables(VariableGroup group)
        {
            group.ChildVariables.AddRange(group.AnalogVariables);
            group.ChildVariables.AddRange(group.DigitalVariables);
            group.ChildVariables.AddRange(group.TextVariables);
            foreach (var childGroup in group.ChildGroups)
            {
                synChildVariables(childGroup);
            }
        }
        
        #endregion

        #region 保存set集合到数据库

        /// <summary>
        /// 保存集合更改
        /// </summary>
        public void Save()
        {
            base.SaveChanges();
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VariableGroup>().Ignore(p => p.ChildVariables);
            
        }
    }
}
