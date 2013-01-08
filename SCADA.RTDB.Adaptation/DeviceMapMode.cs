using SCADA.RTDB.SimulationDeviceBase;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.Adaptation
{
    public class DeviceMapMode : DeviceBase
    {
        public int DeviceModeId { get; set; }
        public VariableBase Variable { get; set; }
        
        //public int VariableId
        //{
        //    get
        //    {
        //        if (Variable == null)
        //        {
        //            return null;
        //        }
        //        return Variable.UniqueId;
        //    }
        //    set { Variable}
        //}
    }
}