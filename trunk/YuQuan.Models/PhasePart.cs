using System.Collections.Generic;
using System.Linq;

namespace YuQuan.Models
{       
    public partial class PhaseDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskType">any of "医嘱", "检查", "检验", "手术", "诊疗工作"</param>
        /// <returns></returns>
        public IEnumerable<TaskDefinition> GetTasks(string taskType)
        {
            var list = new List<TaskDefinition>();

            if (string.IsNullOrEmpty(taskType))
                return list;

            EnumTaskType type=EnumTaskType.医嘱;
            if (taskType.Contains(EnumTaskType.检查.ToString()))
                type = EnumTaskType.检查;
            else if(taskType.Contains(EnumTaskType.检验.ToString()))
                type = EnumTaskType.检验;            
            else if(taskType.Contains(EnumTaskType.手术.ToString()))
                type = EnumTaskType.手术;
            else if(taskType.Contains(EnumTaskType.诊疗工作.ToString()))
                type = EnumTaskType.诊疗工作;

            foreach (var task in TaskDefinition)
            {
                // judge task type by its first child order type
                if(task.MedicalOrderDefinition.Count()>0)
                {
                    if(task.MedicalOrderDefinition.ElementAt(0).MapToTaskType() == type)
                        list.Add(task);
                }
            }

            return list;
        }
    }
}
