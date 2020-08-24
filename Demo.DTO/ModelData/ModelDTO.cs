using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DTO.ModelData
{
    public class ModelDTO
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SoreField { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string SoreMode { get; set; }

    }
}
