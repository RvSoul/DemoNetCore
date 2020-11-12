using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.DTO;
using Demo.DTO.BasicsDataDTO;
using Demo.IDomian.IDM;
using LinuxDemo.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace LinuxDemo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api")]
    [ApiController]
    public class TextController : ApiControllerBase
    {
        //private BasicsDataDMImp dm =new BasicsDataDMImp();

        private readonly IBasicsDataDM dm;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DM"></param>
        public TextController(IBasicsDataDM DM)
        {
            dm = DM;
        }

        /// <summary>
        /// 获取线路列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetXianluList")]
        public ResultEntity<List<XianluDTO>> GetXianluList()
        {
            LogUtil.Info("接口：GetXianluList调用。");
            return new ResultEntityUtil<List<XianluDTO>>().Success(dm.GetXianluList(out int count), count);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetXianluList2")]
        public ActionResult GetXianluList2()
        {
            return Ok(new ResultEntityUtil<List<XianluDTO>>().Success(dm.GetXianluList(out int count), count));
        }

        /// <summary>
        /// 获取标段列表
        /// </summary>
        /// <param name="XianluId"></param>
        /// <returns></returns>
        [HttpGet("GetBiaoduanList")]
        public ResultEntity<List<BiaoduanDTO>> GetBiaoduanList(Guid? XianluId)
        {
            return new ResultEntityUtil<List<BiaoduanDTO>>().Success(dm.GetBiaoduanList(XianluId, out int count), count);
        }

        /// <summary>
        /// 获取工点列表
        /// </summary>
        /// <param name="BiaoduanID"></param>
        /// <returns></returns>
        [HttpGet("GetGongdianList")]
        public ResultEntity<List<GongdianDTO>> GetGongdianList(Guid? BiaoduanID)
        {
            return new ResultEntityUtil<List<GongdianDTO>>().Success(dm.GetGongdianList(BiaoduanID, out int count), count);

        }

    }
}
