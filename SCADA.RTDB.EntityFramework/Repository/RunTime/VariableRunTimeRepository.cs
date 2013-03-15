using SCADA.RTDB.Common;
using SCADA.RTDB.Common.Base;
using SCADA.RTDB.Common.Runtime;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.EntityFramework.Repository.RunTime
{
    /// <summary>
    /// 变量运行仓储
    /// </summary>
    public class VariableRunTimeRepository : IVariableRunTimeRepository
    {
        private readonly IVariableRepository _variableRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iVariableRepository"></param>
        public VariableRunTimeRepository(IVariableRepository iVariableRepository)
        {
            _variableRepository = iVariableRepository;
        }

        /// <summary>
        /// 加载参数
        /// </summary>
        public void Load()
        {
            _variableRepository.Load();
        }
        
        /// <summary>
        /// 根据变量Id提供的路径信息，遍历树查找变量
        /// </summary>
        /// <param name="absolutePath">变量全路径</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        public VariableBase FindVariableByPath(string absolutePath)
        {
            return _variableRepository.FindVariableByPath(absolutePath);
        }
    }
}
